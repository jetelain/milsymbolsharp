namespace PmadMilsymbolSelector {

    const validSidc = new RegExp('^[0-9]{20}([0-9]{10})?$');
    const _options: { [id: string]: PmadMilsymbolSelectorOptions; } = {};
    const _instances: { [id: string]: PmadMilsymbolSelectorInstance; } = {};
    let _bookmarksProvider: BookmarksProvider = null;

    interface SymbolsetJson {
        icons: IconJson[];
        modifiers1: ModifierOrAmplifierJson[];
        modifiers2: ModifierOrAmplifierJson[];
        amplifiers: ModifierOrAmplifierJson[];
    }
    
    interface IconJson {
        code: string;
        entity: string[];
    }

    interface ModifierOrAmplifierJson {
        code: string;
        label: string;
    }

    class PmadMilsymbolSelectorOptions implements SetPmadMilsymbolSelectorOptions {
        getSymbolOptions(): ms.SymbolOptions { return {}; }
        symbolUpdatedCallback?(sidc: string, options: ms.SymbolOptions, symbol: ms.Symbol): void;
    }

    export interface SetPmadMilsymbolSelectorOptions {
        getSymbolOptions?(): ms.SymbolOptions;
        symbolUpdatedCallback?(sidc: string, options: ms.SymbolOptions, symbol: ms.Symbol): void;
    }

    export interface BookmarksProvider {
        saveBookmarks(bookmarks: string[]);
        getBookmarksItems(): string[];
        getBookmarksTimestamp(): Date;
    }

    export interface PmadMilsymbolSelectorInstance {
        updatePreview();
        setValue(sidc: string): void;
        getValue(): string;

        addBookmarkButton(sidc: string);
        removeBookmarkButton(sidc: string);
    }

    interface PseudoSelect {
        setValue(value: string): void;
        getValue(): string;
        addEventListener(event: string, listener: EventListener): void;
    }

    class SelectWithChoicesJS implements PseudoSelect {
        private _element: HTMLSelectElement;
        private _choices: Choices;
        constructor(element: HTMLSelectElement, options: ChoiceOptions) {
            this._element = element;
            this._choices = new Choices(element, options);
        }
        setValue(value: string) {
            this._choices.setChoiceByValue(value);
        }
        getValue(): string {
            return this._element.value;
        }
        addEventListener(event: string, listener: EventListener) {
            this._element.addEventListener(event, listener);
        }
        refresh() {
            this._choices.refresh();
        }
        getElement() {
            return this._element;
        }
    }

    class RadioButtons implements PseudoSelect {
        private _name: string;
        constructor(name: string) {
            this._name = name;
        }
        setValue(value: string) {
            if (Array.isArray(value)) {
                value = value[0];
            }
            document.querySelectorAll<HTMLInputElement>("input[type=radio][name='" + this._name + "']").forEach(input => {
                if (input.value === value) {
                    input.checked = true;
                }
                if (input.className == "") { // BS4
                    input.labels.forEach(label => {
                        label.classList.toggle("active", input.value === value);
                    });
                }
            });
        }
        getValue(): string {
            return document.querySelector<HTMLInputElement>("input[type=radio][name='" + this._name + "']:checked").value;
        }
        addEventListener(event: string, listener: EventListener) {
            document.querySelectorAll<HTMLInputElement>("input[type=radio][name='" + this._name + "']").forEach(input => {
                input.addEventListener("click", listener);
            });
        }
    }

    class FlagCheckboxes implements PseudoSelect {
        private _checkboxes: HTMLInputElement[];
        private _flags: number[];
        constructor(checkboxes: HTMLInputElement[], flags: number[]) {
            this._checkboxes = checkboxes;
            this._flags = flags;
        }
        setValue(value: string) {
            let flags = parseInt(value);
            this._checkboxes.forEach((input, idx) => {
                input.checked = (flags & this._flags[idx]) !== 0;
                if (input.className == "") { // BS4
                    input.labels.forEach(label => {
                        label.classList.toggle("active", input.checked);
                    });
                }
            });
        }
        getValue(): string {
            let value = 0;
            this._checkboxes.forEach((input, idx) => {
                if (input.checked) {
                    value |= this._flags[idx];
                }
            });
            return String(value);
        }
        addEventListener(event: string, listener: EventListener) {
            this._checkboxes.forEach(input => {
                input.addEventListener(event, listener);
            });
        }
    }

    export function setOptions(id: string, setOptions: SetPmadMilsymbolSelectorOptions) {
        Object.assign(getOptions(id), setOptions);
        getInstance(id)?.updatePreview();
    }

    export function getOptions(id: string): PmadMilsymbolSelectorOptions {
        let options = _options[id];
        if (!options) {
            options = new PmadMilsymbolSelectorOptions();
            _options[id] = options;
        }
        return options;
    }

    export function getInstance(id: string): PmadMilsymbolSelectorInstance {
        return _instances[id];
    }

    export function updatePreview(id?: string) {
        if (!id) {
            Object.keys(_instances).forEach(updatePreview);
        } else {
            getInstance(id)?.updatePreview();
        }
    }

    function formatOption(result: HTMLDivElement, element: HTMLOptionElement) {
        let entity = (element as any).pmadEntity as string[];
        if (entity) {
            result.innerHTML = "";
            entity.forEach((label, idx) => {
                let span = document.createElement("span");
                if (idx < entity.length - 1) {
                    span.className = "entity-parent";
                }
                span.innerText = label;
                result.append(span);
            });
        }
        let sidc = element.getAttribute("data-sidc");
        if (sidc) {
            let icon = document.createElement("span");
            icon.className = "symbol-icon";
            icon.innerHTML = new ms.Symbol(sidc, { size: 18 }).asSVG();
            result.prepend(icon);
        }
    }

    const choicesConfig: ChoiceOptions = {
        shouldSort: false,
        itemSelectText: "",
        searchResultLimit: -1,
        callbackOnCreateTemplates: () => ({
            item: function (options: any, choice: any, removeItemButton: boolean) {
                let result = Choices.defaults.templates.item.call(this, options, choice, removeItemButton);
                formatOption(result, choice.element);
                return result;
            },
            choice: function (options: any, choice: any, selectText: string, groupName: string) {
                let result = Choices.defaults.templates.choice.call(this, options, choice, selectText, groupName);
                formatOption(result, choice.element);
                return result;
            },
        }),
        fuseOptions: {
            shouldSort: false,
            findAllMatches: true,
            threshold: 0,
            ignoreLocation: true
        }
    };

    function getSelect(id: string): SelectWithChoicesJS {
        let select = document.getElementById(id) as HTMLSelectElement;
        return new SelectWithChoicesJS(select, choicesConfig);
    }

    function getPseudoSelect(id: string, flags?: number[]): PseudoSelect {
        if (document.querySelectorAll<HTMLInputElement>("input[type=radio][name='" + id + "']").length > 0) {
            return new RadioButtons(id);
        }
        if (flags) {
            let checkboxes = flags.map(flag => document.getElementById(id + flag) as HTMLInputElement);
            if (checkboxes.every(checkbox => checkbox)) {
                return new FlagCheckboxes(checkboxes, flags);
            }
        }
        let select = document.getElementById(id) as HTMLSelectElement;
        if (select) {
            return new SelectWithChoicesJS(select, choicesConfig);
        }
        throw new Error("Element not found: " + id);
    }

    // ---- Bookmark management (common to all instances) ----

    let bookmarkItems = JSON.parse(localStorage.getItem("pmad-milsymbol-bookmarks") ?? "[]") as string[];

    function doAddBookmark(sidc: string) {
        bookmarkItems.push(sidc);
        Object.keys(_instances).forEach(id => _instances[id].addBookmarkButton(sidc));
    }
    function doRemoveBookmark(sidc: string) {
        bookmarkItems = bookmarkItems.filter(other => other !== sidc);
        Object.keys(_instances).forEach(id => _instances[id].removeBookmarkButton(sidc));
    }

    function toggleBookmark(sidc: string) {
        if (!validSidc.test(sidc)) {
            return;
        }
        if (bookmarkItems.includes(sidc)) {
            doRemoveBookmark(sidc);
        } else {
            doAddBookmark(sidc);
        }
        saveBookmarks();
    }

    function saveBookmarks() {
        localStorage.setItem("pmad-milsymbol-bookmarks", JSON.stringify(bookmarkItems));
        localStorage.setItem("pmad-milsymbol-bookmarks-timesteamp", new Date().toJSON());
        if (_bookmarksProvider) {
            _bookmarksProvider.saveBookmarks(bookmarkItems);
        }
    }

    function mergeBookmarks(bookmarks: string[]) {
        const addedBookmarks = bookmarks.filter(sidc => !bookmarkItems.includes(sidc));
        if (addedBookmarks.length > 0) {
            addedBookmarks.forEach(doAddBookmark);
            saveBookmarks();
        }
    }

    function doSetBookmarks(bookmarks: string[]): boolean {
        const addedBookmarks = bookmarks.filter(sidc => !bookmarkItems.includes(sidc));
        const removedBookmarks = bookmarkItems.filter(sidc => !bookmarks.includes(sidc));
        addedBookmarks.forEach(doAddBookmark);
        removedBookmarks.forEach(doRemoveBookmark);
        return addedBookmarks.length != 0 || removedBookmarks.length != 0;
    }

    /**
     * Set bookmarks list, save it to localstorage and update all instances in all tabs
     * To propagate a server side change, please do not set a BookmarksProvider, as this will call again the provider
     * @param bookmarks
     */
    export function setBookmarks(bookmarks: string[]) {
        if (doSetBookmarks(bookmarks)) {
            saveBookmarks();
        }
    }

    addEventListener("storage", (event: StorageEvent) => {
        if (event.key == "pmad-milsymbol-bookmarks") {
            // Bookmarks has changed in an other tab
            const jsonData = localStorage.getItem("pmad-milsymbol-bookmarks");
            if (jsonData) {
                doSetBookmarks(JSON.parse(jsonData) as string[]);
            }
        }
    });


    /**
     * Set a way to save and load bookmarks server-side (to allow bookmarks to work across devices)
     * The localstorage is always used. it will be merged with server data if more recent.
     * @param bookmarksProvider
     */
    export function setBookmarksProvider(bookmarksProvider: BookmarksProvider) {
        _bookmarksProvider = bookmarksProvider;
        if (_bookmarksProvider) {
            const timestamp = localStorage.getItem("pmad-milsymbol-bookmarks-timesteamp");
            if (timestamp) {
                const date = new Date(timestamp);
                if (date > _bookmarksProvider.getBookmarksTimestamp()) {
                    // Local storage is more recent, opt for a merge
                    mergeBookmarks(_bookmarksProvider.getBookmarksItems());
                }
                else {
                    // Local storage is older, opt for a set
                    setBookmarks(_bookmarksProvider.getBookmarksItems());
                }
            } else {
                mergeBookmarks(_bookmarksProvider.getBookmarksItems());
            }
        }
    }

    // ---- Each instance logic ----
    export function initialize(baseId: string) {

        const options = getOptions(baseId);

        const input = document.getElementById(baseId) as HTMLInputElement;
        const preview = document.getElementById(baseId + "-preview") as HTMLDivElement;

        const selectId     = getPseudoSelect(baseId + "-id");
        const selectSet    = getPseudoSelect(baseId + "-set");
        const selectStatus = getPseudoSelect(baseId + "-status");
        const selectHq = getPseudoSelect(baseId + "-hq", [1,2,4]);

        const selectIcon = getSelect(baseId + "-icon");
        const selectMod1 = getSelect(baseId + "-mod1");
        const selectMod2 = getSelect(baseId + "-mod2");
        const selectAmp =  getSelect(baseId + "-amp");

        const bookmarks = document.getElementById(baseId + "-bookmarks") as HTMLDivElement;
        const bookmarkItem = bookmarks.querySelector("div.d-none") as HTMLDivElement;
        const bookmarkButton = document.getElementById(baseId + "-add-bookmark");

        let batchUpdate = false;

        function updateBookmarkButton() {
            if (bookmarkItems.includes(input.value)) {
                bookmarkButton.classList.remove("btn-outline-secondary");
                bookmarkButton.classList.add("btn-secondary");
            } else {
                bookmarkButton.classList.add("btn-outline-secondary");
                bookmarkButton.classList.remove("btn-secondary");
            }
        }

        function updatePreview() {
            var symbol = new ms.Symbol(input.value, options.getSymbolOptions());
            preview.innerHTML = symbol.asSVG();
            updateBookmarkButton();
            if (options.symbolUpdatedCallback) {
                options.symbolUpdatedCallback(input.value, options.getSymbolOptions(), symbol);
            }
        }

        function getSelectedSymbol() {
            let symbol = '100';
            // common
            symbol += selectId.getValue() || '0';
            symbol += selectSet.getValue() || '00';
            symbol += selectStatus.getValue() || '0';
            symbol += selectHq.getValue() || '0';
            // depends on set
            symbol += selectAmp.getValue() || '00';
            symbol += selectIcon.getValue() || '000000';
            symbol += selectMod1.getValue() || '00';
            symbol += selectMod2.getValue() || '00';
            return symbol;
        }

        function updateSelects(sidc: string) {
            batchUpdate = true;

            selectId.setValue(sidc.substring(3, 4));
            selectStatus.setValue(sidc.substring(6, 7));
            selectHq.setValue(sidc.substring(7, 8));

            const set = sidc.substring(4, 6);
            if (set != selectSet.getValue()) {
                selectSet.setValue(set);
                batchUpdate = false;
                loadSymbolSet(sidc);
                return;
            }

            selectAmp.setValue(sidc.substring(8, 10));
            selectIcon.setValue(sidc.substring(10, 16));
            selectMod1.setValue(sidc.substring(16, 18));
            selectMod2.setValue(sidc.substring(18, 20));
            batchUpdate = false;

        }

        function updateSelectedSymbol() {
            if (!batchUpdate) {
                input.value = getSelectedSymbol();
                updatePreview();
            }
        }

        function generateSelectContent(data: ModifierOrAmplifierJson[], choices: SelectWithChoicesJS, selected: string, getSdic: (code: string) => string) {
            let select = choices.getElement();
            select.innerHTML = '';
            data.forEach(item => {
                const option = document.createElement('option');
                option.value = item.code;
                option.text = item.label;
                option.setAttribute("data-sidc", getSdic(item.code));
                if (item.code === selected) {
                    option.selected = true;
                }
                select.appendChild(option);
            });
            if (data.length == 0) {
                const option = document.createElement('option');
                option.value = '00';
                option.selected = true;
                option.text = '(n/a)';
                select.appendChild(option);
            }
            choices.refresh();
            choices.setValue(selected);
        }

        function generateIconSelectContent(data: IconJson[], choices: SelectWithChoicesJS, selected: string, getSdic: (code: string) => string) {
            let select = choices.getElement();
            select.innerHTML = '';
            let entity0 = '';
            let optgroup: HTMLOptGroupElement;

            data.forEach(item => {
                if (item.entity[0] !== entity0 || !optgroup) {
                    optgroup = document.createElement('optgroup');
                    optgroup.label = item.entity[0];
                    select.appendChild(optgroup);
                    entity0 = item.entity[0];
                }
                const option = document.createElement('option');
                option.value = item.code;
                option.text = item.entity.join(' > ');
                (option as any).pmadEntity = item.entity;
                option.setAttribute("data-sidc", getSdic(item.code));
                if (item.code === selected) {
                    option.selected = true;
                }
                optgroup.appendChild(option);
            });
            choices.refresh();
            choices.setValue(selected);

        }

        async function loadSymbolSet(sidc: string) {
            const set = sidc.substring(4, 6);
            const result = await fetch(`/lib/pmad-milsymbol/app6d/${set}.json`);
            const json = (await result.json()) as SymbolsetJson;
            batchUpdate = true;
            generateSelectContent(json.amplifiers, selectAmp, sidc.substring(8, 10), code => `1003${set}00${code}0000000000`);
            generateSelectContent(json.modifiers1, selectMod1, sidc.substring(16, 18), code => `1003${set}0000000000${code}00`);
            generateSelectContent(json.modifiers2, selectMod2, sidc.substring(18, 20), code => `1003${set}000000000000${code}`);
            generateIconSelectContent(json.icons, selectIcon, sidc.substring(10, 16), code => `1003${set}0000${code}0000`);
            input.value = getSelectedSymbol(); // sidc might not be valid according to the new set, update it
            batchUpdate = false;
            updatePreview();
        }

        function updateSelectedSymbolSet() {
            if (!batchUpdate) {
                const sdic = getSelectedSymbol().substring(0, 8) + "000000000000";
                input.value = sdic;
                loadSymbolSet(sdic);
            }
        }

        const bookmarkButtons = {};

        function addBookmarkButton(sidc: string) {
            let item = bookmarkItem.cloneNode(true) as HTMLDivElement;
            item.classList.remove("d-none");
            let button = item.querySelector("button");
            button.innerHTML = new ms.Symbol(sidc, { size: 18 }).asSVG();
            button.title = sidc;
            button.addEventListener("click", function () {
                input.value = sidc;
                updateSelects(sidc);
                updatePreview();
            });
            bookmarks.appendChild(item);
            bookmarkButtons[sidc] = item;
            updateBookmarkButton();
        }

        function removeBookmarkButton(sidc: string) {
            bookmarkButtons[sidc]?.remove();
            updateBookmarkButton();
        }

        updateSelects(input.value);

        selectId.addEventListener("change", updateSelectedSymbol);
        selectSet.addEventListener("change", updateSelectedSymbolSet);
        selectStatus.addEventListener("change", updateSelectedSymbol);
        selectHq.addEventListener("change", updateSelectedSymbol);
        selectIcon.addEventListener("change", updateSelectedSymbol);
        selectMod1.addEventListener("change", updateSelectedSymbol);
        selectMod2.addEventListener("change", updateSelectedSymbol);
        selectAmp.addEventListener("change", updateSelectedSymbol);

        input.addEventListener("change", function () {
            updateSelects(input.value);
            updatePreview();
        });

        loadSymbolSet(input.value);

        updatePreview();

        document.getElementById(baseId + "-copy-code").addEventListener("click", function () {
            navigator.clipboard.writeText(input.value);
        });

        document.getElementById(baseId + "-copy-image").addEventListener("click", async function () {
            const canvas = new ms.Symbol(input.value, options.getSymbolOptions()).asCanvas(2);
            canvas.toBlob(blob => {
                const item = new ClipboardItem({ "image/png": blob });
                navigator.clipboard.write([item]);
            });
        });

        bookmarkItems.forEach(sidc => {
            addBookmarkButton(sidc);
        });

        bookmarkButton.addEventListener("click", function () {
            toggleBookmark(input.value);
        });

        _instances[baseId] = {
            updatePreview: updatePreview,
            setValue: function (sidc: string) {
                input.value = sidc;
                input.dispatchEvent(new Event("change"));
            },
            getValue: () => input.value,
            addBookmarkButton: addBookmarkButton,
            removeBookmarkButton: removeBookmarkButton
        };
    }

    interface SymbolBookmark {
        sidc: string;
        label?: string;
    }
    interface SymbolBookmarksData {
        bookmarks: SymbolBookmark[];
        timestamp: string;
        endpoint: string;
        token: string;
    }

    class BuiltinBookmarksProvider implements BookmarksProvider {
        items: string[];
        timestamp: Date;
        token: string;
        endpoint: string;

        constructor(data: SymbolBookmarksData) {
            this.items = data.bookmarks.map(b => b.sidc);
            this.timestamp = data.timestamp ? new Date(data.timestamp) : new Date(0);
            this.token = data.token;
            this.endpoint = data.endpoint;
        }

        saveBookmarks(bookmarks: string[]) {
            fetch(this.endpoint, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded'
                },
                body: new URLSearchParams({
                    '__RequestVerificationToken': this.token,
                    'bookmarks': JSON.stringify(bookmarks)
                })
            }).then(response => {
                if (!response.ok) {
                    console.error('Failed to save bookmarks');
                }
            }).catch(error => {
                console.error('Error:', error);
            });
        }
        getBookmarksItems(): string[] {
            return this.items;
        }
        getBookmarksTimestamp(): Date {
            return this.timestamp;
        }
    }

    export function initializePage() {
        ms.setStandard("APP6");

        let body = document.querySelector("body");
        let bookmarks = body?.getAttribute('data-pmad-milsymbol-bookmarks');
        if (bookmarks) {
            setBookmarksProvider(new BuiltinBookmarksProvider(JSON.parse(bookmarks)));
        }

        document.querySelectorAll("div.pmad-symbol-selector").forEach(element => {
            initialize(element.getAttribute("data-base-id"));
        });
    }
}

document.addEventListener("DOMContentLoaded", PmadMilsymbolSelector.initializePage);

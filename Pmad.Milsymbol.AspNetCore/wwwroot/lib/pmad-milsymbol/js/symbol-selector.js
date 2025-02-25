var PmadMilsymbolSelector;
(function (PmadMilsymbolSelector) {
    var _a;
    const validSidc = new RegExp('^[0-9]{20}([0-9]{10})?$');
    const _options = {};
    const _instances = {};
    let _bookmarksProvider = null;
    class PmadMilsymbolSelectorOptions {
        getSymbolOptions() { return {}; }
    }
    class Constant {
        constructor(value) {
            this._value = value;
        }
        setValue(value) {
        }
        getValue() {
            return this._value;
        }
        addEventListener(event, listener) {
        }
    }
    class SelectWithChoicesJS {
        constructor(element, options) {
            this._element = element;
            this._choices = new Choices(element, options);
        }
        setValue(value) {
            this._choices.setChoiceByValue(value);
        }
        getValue() {
            return this._element.value;
        }
        addEventListener(event, listener) {
            this._element.addEventListener(event, listener);
        }
        refresh() {
            this._choices.refresh();
        }
        getElement() {
            return this._element;
        }
    }
    class RadioButtons {
        constructor(name) {
            this._name = name;
        }
        setValue(value) {
            if (Array.isArray(value)) {
                value = value[0];
            }
            document.querySelectorAll("input[type=radio][name='" + this._name + "']").forEach(input => {
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
        getValue() {
            return document.querySelector("input[type=radio][name='" + this._name + "']:checked").value;
        }
        addEventListener(event, listener) {
            document.querySelectorAll("input[type=radio][name='" + this._name + "']").forEach(input => {
                input.addEventListener("click", listener);
            });
        }
    }
    class FlagCheckboxes {
        constructor(checkboxes, flags) {
            this._checkboxes = checkboxes;
            this._flags = flags;
        }
        setValue(value) {
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
        getValue() {
            let value = 0;
            this._checkboxes.forEach((input, idx) => {
                if (input.checked) {
                    value |= this._flags[idx];
                }
            });
            return String(value);
        }
        addEventListener(event, listener) {
            this._checkboxes.forEach(input => {
                input.addEventListener(event, listener);
            });
        }
    }
    function setOptions(id, setOptions) {
        var _a;
        Object.assign(getOptions(id), setOptions);
        (_a = getInstance(id)) === null || _a === void 0 ? void 0 : _a.updatePreview();
    }
    PmadMilsymbolSelector.setOptions = setOptions;
    function getOptions(id) {
        let options = _options[id];
        if (!options) {
            options = new PmadMilsymbolSelectorOptions();
            _options[id] = options;
        }
        return options;
    }
    PmadMilsymbolSelector.getOptions = getOptions;
    function getInstance(id) {
        return _instances[id];
    }
    PmadMilsymbolSelector.getInstance = getInstance;
    function updatePreview(id) {
        var _a;
        if (!id) {
            Object.keys(_instances).forEach(updatePreview);
        }
        else {
            (_a = getInstance(id)) === null || _a === void 0 ? void 0 : _a.updatePreview();
        }
    }
    PmadMilsymbolSelector.updatePreview = updatePreview;
    function formatOption(result, element) {
        let entity = element.pmadEntity;
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
    const choicesConfig = {
        shouldSort: false,
        itemSelectText: "",
        searchResultLimit: -1,
        callbackOnCreateTemplates: () => ({
            item: function (options, choice, removeItemButton) {
                let result = Choices.defaults.templates.item.call(this, options, choice, removeItemButton);
                formatOption(result, choice.element);
                return result;
            },
            choice: function (options, choice, selectText, groupName) {
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
    function getSelect(id) {
        let select = document.getElementById(id);
        return new SelectWithChoicesJS(select, choicesConfig);
    }
    function getPseudoSelect(id, flags) {
        if (document.querySelectorAll("input[type=radio][name='" + id + "']").length > 0) {
            return new RadioButtons(id);
        }
        if (flags) {
            let checkboxes = flags.map(flag => document.getElementById(id + flag));
            if (checkboxes.every(checkbox => checkbox)) {
                return new FlagCheckboxes(checkboxes, flags);
            }
        }
        let element = document.getElementById(id);
        if (element) {
            if (element.tagName.toUpperCase() === "SELECT") {
                return new SelectWithChoicesJS(element, choicesConfig);
            }
            return new Constant(element.value);
        }
        throw new Error("Element not found: " + id);
    }
    // ---- Bookmark management (common to all instances) ----
    let bookmarkItems = JSON.parse((_a = localStorage.getItem("pmad-milsymbol-bookmarks")) !== null && _a !== void 0 ? _a : "[]");
    function doAddBookmark(sidc) {
        bookmarkItems.push(sidc);
        Object.keys(_instances).forEach(id => _instances[id].addBookmarkButton(sidc));
    }
    function doRemoveBookmark(sidc) {
        bookmarkItems = bookmarkItems.filter(other => other !== sidc);
        Object.keys(_instances).forEach(id => _instances[id].removeBookmarkButton(sidc));
    }
    function toggleBookmark(sidc) {
        if (!validSidc.test(sidc)) {
            return;
        }
        if (bookmarkItems.includes(sidc)) {
            doRemoveBookmark(sidc);
        }
        else {
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
    function mergeBookmarks(bookmarks) {
        const addedBookmarks = bookmarks.filter(sidc => !bookmarkItems.includes(sidc));
        if (addedBookmarks.length > 0) {
            addedBookmarks.forEach(doAddBookmark);
            saveBookmarks();
        }
    }
    function doSetBookmarks(bookmarks) {
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
    function setBookmarks(bookmarks) {
        if (doSetBookmarks(bookmarks)) {
            saveBookmarks();
        }
    }
    PmadMilsymbolSelector.setBookmarks = setBookmarks;
    addEventListener("storage", (event) => {
        if (event.key == "pmad-milsymbol-bookmarks") {
            // Bookmarks has changed in an other tab
            const jsonData = localStorage.getItem("pmad-milsymbol-bookmarks");
            if (jsonData) {
                doSetBookmarks(JSON.parse(jsonData));
            }
        }
    });
    /**
     * Set a way to save and load bookmarks server-side (to allow bookmarks to work across devices)
     * The localstorage is always used. it will be merged with server data if more recent.
     * @param bookmarksProvider
     */
    function setBookmarksProvider(bookmarksProvider) {
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
            }
            else {
                mergeBookmarks(_bookmarksProvider.getBookmarksItems());
            }
        }
    }
    PmadMilsymbolSelector.setBookmarksProvider = setBookmarksProvider;
    // ---- Each instance logic ----
    function initialize(baseId) {
        const options = getOptions(baseId);
        const input = document.getElementById(baseId);
        const preview = document.getElementById(baseId + "-preview");
        const selectId = getPseudoSelect(baseId + "-id");
        const selectSet = getPseudoSelect(baseId + "-set");
        const selectStatus = getPseudoSelect(baseId + "-status");
        const selectHq = getPseudoSelect(baseId + "-hq", [1, 2, 4]);
        const selectIcon = getSelect(baseId + "-icon");
        const selectMod1 = getSelect(baseId + "-mod1");
        const selectMod2 = getSelect(baseId + "-mod2");
        const selectAmp = getSelect(baseId + "-amp");
        const bookmarks = document.getElementById(baseId + "-bookmarks");
        const bookmarkItem = bookmarks.querySelector("div.d-none");
        const bookmarkButton = document.getElementById(baseId + "-add-bookmark");
        let batchUpdate = false;
        function updateBookmarkButton() {
            if (bookmarkItems.includes(input.value)) {
                bookmarkButton.classList.remove("btn-outline-secondary");
                bookmarkButton.classList.add("btn-secondary");
            }
            else {
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
        function updateSelects(sidc) {
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
        function generateSelectContent(data, choices, selected, getSdic) {
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
        function generateIconSelectContent(data, choices, selected, getSdic) {
            let select = choices.getElement();
            select.innerHTML = '';
            let entity0 = '';
            let optgroup;
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
                option.pmadEntity = item.entity;
                option.setAttribute("data-sidc", getSdic(item.code));
                if (item.code === selected) {
                    option.selected = true;
                }
                optgroup.appendChild(option);
            });
            choices.refresh();
            choices.setValue(selected);
        }
        async function loadSymbolSet(sidc) {
            const set = sidc.substring(4, 6);
            const result = await fetch(`/lib/pmad-milsymbol/app6d/${set}.json`);
            const json = (await result.json());
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
        function addBookmarkButton(sidc) {
            let item = bookmarkItem.cloneNode(true);
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
        function removeBookmarkButton(sidc) {
            var _a;
            (_a = bookmarkButtons[sidc]) === null || _a === void 0 ? void 0 : _a.remove();
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
            setValue: function (sidc) {
                input.value = sidc;
                input.dispatchEvent(new Event("change"));
            },
            getValue: () => input.value,
            addBookmarkButton: addBookmarkButton,
            removeBookmarkButton: removeBookmarkButton
        };
    }
    PmadMilsymbolSelector.initialize = initialize;
    class BuiltinBookmarksProvider {
        constructor(data) {
            this.items = data.bookmarks.map(b => b.sidc);
            this.timestamp = data.timestamp ? new Date(data.timestamp) : new Date(0);
            this.token = data.token;
            this.endpoint = data.endpoint;
        }
        saveBookmarks(bookmarks) {
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
        getBookmarksItems() {
            return this.items;
        }
        getBookmarksTimestamp() {
            return this.timestamp;
        }
    }
    function initializePage() {
        ms.setStandard("APP6");
        let body = document.querySelector("body");
        let bookmarks = body === null || body === void 0 ? void 0 : body.getAttribute('data-pmad-milsymbol-bookmarks');
        if (bookmarks) {
            setBookmarksProvider(new BuiltinBookmarksProvider(JSON.parse(bookmarks)));
        }
        document.querySelectorAll("div.pmad-symbol-selector").forEach(element => {
            initialize(element.getAttribute("data-base-id"));
        });
    }
    PmadMilsymbolSelector.initializePage = initializePage;
})(PmadMilsymbolSelector || (PmadMilsymbolSelector = {}));
document.addEventListener("DOMContentLoaded", PmadMilsymbolSelector.initializePage);

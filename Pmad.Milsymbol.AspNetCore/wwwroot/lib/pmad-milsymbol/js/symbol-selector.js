var PmadMilsymbolSelector;
(function (PmadMilsymbolSelector) {
    const validSidc = new RegExp('^([0-9]{20})|([0-9]{30})$');
    const _options = {};
    const _instances = {};
    class PmadMilsymbolSelectorOptions {
        getSymbolOptions() { return {}; }
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
        if (setOptions.getBookmarks) {
            // merge bookmarks if already initialized
            (_a = getInstance(id)) === null || _a === void 0 ? void 0 : _a.mergeBookmarks(setOptions.getBookmarks());
        }
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
        let select = document.getElementById(id);
        if (select) {
            return new SelectWithChoicesJS(select, choicesConfig);
        }
        throw new Error("Element not found: " + id);
    }
    function initialize(baseId) {
        var _a;
        const options = getOptions(baseId);
        let bookmarkItems = JSON.parse((_a = localStorage.getItem("pmad-milsymbol-bookmarks")) !== null && _a !== void 0 ? _a : "[]");
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
        function saveBookmarks() {
            localStorage.setItem("pmad-milsymbol-bookmarks", JSON.stringify(bookmarkItems));
            if (options.saveBookmarks) {
                options.saveBookmarks(bookmarkItems);
            }
        }
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
            preview.innerHTML = new ms.Symbol(input.value, options.getSymbolOptions()).asSVG();
            updateBookmarkButton();
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
            selectSet.setValue(sidc.substring(4, 6));
            selectStatus.setValue(sidc.substring(6, 7));
            selectHq.setValue(sidc.substring(7, 8));
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
                option.text = item.entity.join(' - ');
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
            input.value = sidc;
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
            const msOptions = options.getSymbolOptions();
            msOptions.size = 200; // twice the default, make a setting ?
            const canvas = new ms.Symbol(input.value, msOptions).asCanvas();
            canvas.toBlob(blob => {
                const item = new ClipboardItem({ "image/png": blob });
                navigator.clipboard.write([item]);
            });
        });
        bookmarkItems.forEach(sidc => {
            addBookmarkButton(sidc);
        });
        bookmarkButton.addEventListener("click", function () {
            if (!bookmarkItems.includes(input.value)) {
                bookmarkItems.push(input.value);
                addBookmarkButton(input.value);
            }
            else {
                bookmarkItems = bookmarkItems.filter(sidc => sidc !== input.value);
                removeBookmarkButton(input.value);
            }
            saveBookmarks();
        });
        function mergeBookmarks(bookmarks) {
            let changed = bookmarks.length !== bookmarkItems.length;
            bookmarks.forEach(sidc => {
                if (!bookmarkItems.includes(sidc)) {
                    bookmarkItems.push(sidc);
                    addBookmarkButton(sidc);
                    changed = true;
                }
            });
            if (changed) {
                saveBookmarks();
            }
        }
        if (options.getBookmarks) {
            mergeBookmarks(options.getBookmarks());
        }
        _instances[baseId] = {
            updatePreview: updatePreview,
            mergeBookmarks: mergeBookmarks,
            setValue: function (sidc) {
                input.value = sidc;
                input.dispatchEvent(new Event("change"));
            },
            getValue: () => input.value
        };
    }
    PmadMilsymbolSelector.initialize = initialize;
})(PmadMilsymbolSelector || (PmadMilsymbolSelector = {}));
document.addEventListener("DOMContentLoaded", function () {
    ms.setStandard("APP6");
    document.querySelectorAll("div.pmad-symbol-selector").forEach(element => {
        PmadMilsymbolSelector.initialize(element.getAttribute("data-base-id"));
    });
});

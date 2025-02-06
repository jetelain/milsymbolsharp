class PmadMilsymbolSelectorOptions {
    getSymbolOptions() { return {}; }
}
class PmadMilsymbolSelector {
    static setOptions(id, setOptions) {
        var _a;
        Object.assign(PmadMilsymbolSelector.getOptions(id), setOptions);
        if (setOptions.getBookmarks) {
            // merge bookmarks if already initialized
            (_a = PmadMilsymbolSelector.get(id)) === null || _a === void 0 ? void 0 : _a.mergeBookmarks(setOptions.getBookmarks());
        }
    }
    static getOptions(id) {
        let options = PmadMilsymbolSelector._options[id];
        if (!options) {
            options = new PmadMilsymbolSelectorOptions();
            PmadMilsymbolSelector._options[id] = options;
        }
        return options;
    }
    static get(id) {
        return PmadMilsymbolSelector._instances[id];
    }
    static updatePreview(id) {
        var _a;
        if (!id) {
            Object.keys(PmadMilsymbolSelector._instances).forEach(PmadMilsymbolSelector.updatePreview);
        }
        else {
            (_a = PmadMilsymbolSelector.get(id)) === null || _a === void 0 ? void 0 : _a.updatePreview();
        }
    }
    static initialize(baseId) {
        var _a;
        let bookmarkItems = JSON.parse((_a = localStorage.getItem("pmad-milsymbol-bookmarks")) !== null && _a !== void 0 ? _a : "[]");
        const input = document.getElementById(baseId);
        const preview = document.getElementById(baseId + "-preview");
        const selectId = document.getElementById(baseId + "-id");
        const selectSet = document.getElementById(baseId + "-set");
        const selectStatus = document.getElementById(baseId + "-status");
        const selectHq = document.getElementById(baseId + "-hq");
        const selectIcon = document.getElementById(baseId + "-icon");
        const selectMod1 = document.getElementById(baseId + "-mod1");
        const selectMod2 = document.getElementById(baseId + "-mod2");
        const selectAmp = document.getElementById(baseId + "-amp");
        const bookmarks = document.getElementById(baseId + "-bookmarks");
        const bookmarkItem = bookmarks.querySelector("div.d-none");
        const options = PmadMilsymbolSelector.getOptions(baseId);
        const bookmarkButton = document.getElementById(baseId + "-add-bookmark");
        function saveBookmarks() {
            localStorage.setItem("pmad-milsymbol-bookmarks", JSON.stringify(bookmarkItems));
            if (options.saveBookmarks) {
                options.saveBookmarks(bookmarkItems);
            }
        }
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
        const choicesId = new Choices(selectId, choicesConfig);
        const choicesSet = new Choices(selectSet, choicesConfig);
        const choicesStatus = new Choices(selectStatus, choicesConfig);
        const choicesHq = new Choices(selectHq, choicesConfig);
        const choicesIcon = new Choices(selectIcon, choicesConfig);
        const choicesMod1 = new Choices(selectMod1, choicesConfig);
        const choicesMod2 = new Choices(selectMod2, choicesConfig);
        const choicesAmp = new Choices(selectAmp, choicesConfig);
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
            symbol += selectId.value || '0';
            symbol += selectSet.value || '00';
            symbol += selectStatus.value || '0';
            symbol += selectHq.value || '0';
            // depends on set
            symbol += selectAmp.value || '00';
            symbol += selectIcon.value || '000000';
            symbol += selectMod1.value || '00';
            symbol += selectMod2.value || '00';
            return symbol;
        }
        function updateSelects(sidc) {
            batchUpdate = true;
            choicesId.setChoiceByValue(sidc.substring(3, 4));
            choicesSet.setChoiceByValue(sidc.substring(4, 6));
            choicesStatus.setChoiceByValue(sidc.substring(6, 7));
            choicesHq.setChoiceByValue(sidc.substring(7, 8));
            choicesAmp.setChoiceByValue(sidc.substring(8, 10));
            choicesIcon.setChoiceByValue(sidc.substring(10, 16));
            choicesMod1.setChoiceByValue(sidc.substring(16, 18));
            choicesMod2.setChoiceByValue(sidc.substring(18, 20));
            batchUpdate = false;
        }
        function updateSelectedSymbol() {
            if (!batchUpdate) {
                input.value = getSelectedSymbol();
                updatePreview();
            }
        }
        function generateSelectContent(data, select, choices, selected, getSdic) {
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
            choices.setChoiceByValue(selected);
        }
        function generateIconSelectContent(data, select, choices, selected, getSdic) {
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
            choices.setChoiceByValue(selected);
        }
        async function loadSymbolSet(sidc) {
            const set = sidc.substring(4, 6);
            const result = await fetch(`/lib/pmad-milsymbol/app6d/${set}.json`);
            const json = (await result.json());
            batchUpdate = true;
            generateSelectContent(json.amplifiers, selectAmp, choicesAmp, sidc.substring(8, 10), code => `1003${set}00${code}0000000000`);
            generateSelectContent(json.modifiers1, selectMod1, choicesMod1, sidc.substring(16, 18), code => `1003${set}0000000000${code}00`);
            generateSelectContent(json.modifiers2, selectMod2, choicesMod2, sidc.substring(18, 20), code => `1003${set}000000000000${code}`);
            generateIconSelectContent(json.icons, selectIcon, choicesIcon, sidc.substring(10, 16), code => `1003${set}0000${code}0000`);
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
        PmadMilsymbolSelector._instances[baseId] = {
            updatePreview: updatePreview,
            mergeBookmarks: mergeBookmarks,
            setValue: function (sidc) {
                input.value = sidc;
                input.dispatchEvent(new Event("change"));
            },
            getValue: () => input.value
        };
    }
}
PmadMilsymbolSelector._options = {};
PmadMilsymbolSelector._instances = {};
document.addEventListener("DOMContentLoaded", function () {
    ms.setStandard("APP6");
    document.querySelectorAll("div.pmad-symbol-selector").forEach(element => {
        PmadMilsymbolSelector.initialize(element.getAttribute("data-base-id"));
    });
});

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
    saveBookmarks?(bookmarks: string[]): void;
    getBookmarks?(): string[];
}

interface SetPmadMilsymbolSelectorOptions {
    getSymbolOptions?(): ms.SymbolOptions;
    saveBookmarks?(bookmarks: string[]): void;
    getBookmarks?(): string[];
}

interface PmadMilsymbolSelectorInstance {
    updatePreview();
    mergeBookmarks(bookmarks: string[]): void;
    setValue(sidc: string): void;
    getValue(): string;
}

class PmadMilsymbolSelector {
    private static _options: { [id: string]: PmadMilsymbolSelectorOptions; } = {};
    private static _instances: { [id: string]: PmadMilsymbolSelectorInstance; } = {};

    static setOptions(id: string, setOptions: SetPmadMilsymbolSelectorOptions) {
        Object.assign(PmadMilsymbolSelector.getOptions(id), setOptions);
        if (setOptions.getBookmarks) {
            // merge bookmarks if already initialized
            PmadMilsymbolSelector.get(id)?.mergeBookmarks(setOptions.getBookmarks());
        }
    }

    static getOptions(id: string): PmadMilsymbolSelectorOptions {
        let options = PmadMilsymbolSelector._options[id];
        if (!options) {
            options = new PmadMilsymbolSelectorOptions();
            PmadMilsymbolSelector._options[id] = options;
        }
        return options;
    }

    static get(id: string): PmadMilsymbolSelectorInstance {
        return PmadMilsymbolSelector._instances[id];
    }

    static updatePreview(id?: string) {
        if (!id) {
            Object.keys(PmadMilsymbolSelector._instances).forEach(PmadMilsymbolSelector.updatePreview);
        } else {
            PmadMilsymbolSelector.get(id)?.updatePreview();
        }
    }

    static initialize(baseId: string) {

        let bookmarkItems = JSON.parse(localStorage.getItem("pmad-milsymbol-bookmarks") ?? "[]") as string[];

        const input = document.getElementById(baseId) as HTMLInputElement;
        const preview = document.getElementById(baseId + "-preview") as HTMLDivElement;
        const selectId = document.getElementById(baseId + "-id") as HTMLSelectElement;
        const selectSet = document.getElementById(baseId + "-set") as HTMLSelectElement;
        const selectStatus = document.getElementById(baseId + "-status") as HTMLSelectElement;
        const selectHq = document.getElementById(baseId + "-hq") as HTMLSelectElement;
        const selectIcon = document.getElementById(baseId + "-icon") as HTMLSelectElement;
        const selectMod1 = document.getElementById(baseId + "-mod1") as HTMLSelectElement;
        const selectMod2 = document.getElementById(baseId + "-mod2") as HTMLSelectElement;
        const selectAmp = document.getElementById(baseId + "-amp") as HTMLSelectElement;
        const bookmarks = document.getElementById(baseId + "-bookmarks") as HTMLDivElement;
        const bookmarkItem = bookmarks.querySelector("div.d-none") as HTMLDivElement;
        const options = PmadMilsymbolSelector.getOptions(baseId);
        const bookmarkButton = document.getElementById(baseId + "-add-bookmark");

        function saveBookmarks() {
            localStorage.setItem("pmad-milsymbol-bookmarks", JSON.stringify(bookmarkItems));
            if (options.saveBookmarks) {
                options.saveBookmarks(bookmarkItems);
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
            } else {
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

        function updateSelects(sidc: string) {
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

        function generateSelectContent(data: ModifierOrAmplifierJson[], select: HTMLSelectElement, choices: Choices, selected: string, getSdic: (code: string) => string) {
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

        function generateIconSelectContent(data: IconJson[], select: HTMLSelectElement, choices: Choices, selected: string, getSdic: (code: string) => string) {
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
                option.text = item.entity.join(' - ');
                (option as any).pmadEntity = item.entity;
                option.setAttribute("data-sidc", getSdic(item.code));
                if (item.code === selected) {
                    option.selected = true;
                }
                optgroup.appendChild(option);
            });
            choices.refresh();
            choices.setChoiceByValue(selected);

        }

        async function loadSymbolSet(sidc: string) {
            const set = sidc.substring(4, 6);
            const result = await fetch(`/lib/pmad-milsymbol/app6d/${set}.json`);
            const json = (await result.json()) as SymbolsetJson;
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
            } else {
                bookmarkItems = bookmarkItems.filter(sidc => sidc !== input.value);
                removeBookmarkButton(input.value);
            }
            saveBookmarks();
        });

        function mergeBookmarks(bookmarks: string[]) {
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
            setValue: function (sidc: string) {
                input.value = sidc;
                input.dispatchEvent(new Event("change"));
            },
            getValue: () => input.value
        };
    }
}

document.addEventListener("DOMContentLoaded", function () {
    ms.setStandard("APP6");
    document.querySelectorAll("div.pmad-symbol-selector").forEach(element => {
        PmadMilsymbolSelector.initialize(element.getAttribute("data-base-id"));
    });
});

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

//document.addEventListener("load", function () {

    ms.setStandard("APP6");

    document.querySelectorAll("div.pmad-symbol-selector").forEach(element => {

        const baseId = element.getAttribute("data-base-id");

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

        function addIcon(result: HTMLDivElement, element: HTMLOptionElement) {
            let sidc = element.getAttribute("data-sidc");
            if (sidc) {
                let icon = document.createElement("span");
                icon.className = "select-symbol-icon";
                icon.innerHTML = new ms.Symbol(sidc, { size: 18 }).asSVG();
                result.prepend(icon);
            }
        }

        const choicesConfig: ChoiceOptions = {
            shouldSort: false,
            itemSelectText: "",
            searchResultLimit: -1,
            callbackOnCreateTemplates: (strToEl, escapeForTemplate, getClassNames) => ({
                item: function (options: any, choice: any, removeItemButton: boolean) {
                    let result = Choices.defaults.templates.item.call(this, options, choice, removeItemButton);
                    addIcon(result, choice.element);
                    return result;
                },
                choice: function (options: any, choice: any, selectText: string, groupName: string) {
                    let result = Choices.defaults.templates.choice.call(this, options, choice, selectText, groupName);
                    addIcon(result, choice.element);
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

        function updatePreview() {
            preview.innerHTML = new ms.Symbol(input.value, {}).asSVG();
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
            //selectId.value = sidc.substring(3, 4);
            //selectSet.value = sidc.substring(4, 6);
            //selectStatus.value = sidc.substring(6, 7);
            //selectHq.value = sidc.substring(7, 8);
            //selectAmp.value = sidc.substring(8, 10);
            //selectIcon.value = sidc.substring(10, 16);
            //selectMod1.value = sidc.substring(16, 18);
            //selectMod2.value = sidc.substring(18, 20);
            batchUpdate = false;
        }

        function updateSelectedSymbol() {
            if (!batchUpdate) {
                input.value = getSelectedSymbol();
                updatePreview();
            }
        }

        function generateSelectContent(data: ModifierOrAmplifierJson[], select: HTMLSelectElement, choices: Choices, selected: string, getSdic: (code:string) => string) {
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
            generateSelectContent(json.amplifiers, selectAmp, choicesAmp, sidc.substring(8, 10),    code => `1003${set}00${code}0000000000`);
            generateSelectContent(json.modifiers1, selectMod1, choicesMod1, sidc.substring(16, 18), code => `1003${set}0000000000${code}00`);
            generateSelectContent(json.modifiers2, selectMod2, choicesMod2, sidc.substring(18, 20), code => `1003${set}000000000000${code}`);
            generateIconSelectContent(json.icons, selectIcon, choicesIcon, sidc.substring(10, 16),  code => `1003${set}0000${code}0000`);
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
    });

//});
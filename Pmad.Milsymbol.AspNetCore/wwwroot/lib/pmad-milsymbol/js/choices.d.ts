// Options interface
declare interface ChoiceOptions {
    silent?: boolean;
    items?: any[];
    choices?: any[];
    renderChoiceLimit?: number;
    maxItemCount?: number;
    closeDropdownOnSelect?: boolean | 'auto';
    singleModeForMultiSelect?: boolean;
    addChoices?: boolean;
    addItems?: boolean;
    addItemFilter?: (value: string) => boolean;
    removeItems?: boolean;
    removeItemButton?: boolean;
    removeItemButtonAlignLeft?: boolean;
    editItems?: boolean;
    allowHTML?: boolean;
    allowHtmlUserInput?: boolean;
    duplicateItemsAllowed?: boolean;
    delimiter?: string;
    paste?: boolean;
    searchEnabled?: boolean;
    searchChoices?: boolean;
    searchFloor?: number;
    searchResultLimit?: number;
    searchFields?: string[];
    position?: 'auto' | 'top' | 'bottom';
    resetScrollPosition?: boolean;
    shouldSort?: boolean;
    shouldSortItems?: boolean;
    sorter?: (a: any, b: any) => number;
    shadowRoot?: ShadowRoot | null;
    placeholder?: boolean;
    placeholderValue?: string | null;
    searchPlaceholderValue?: string | null;
    prependValue?: string | null;
    appendValue?: string | null;
    renderSelectedChoices?: 'auto' | 'always';
    loadingText?: string;
    noResultsText?: string;
    noChoicesText?: string;
    itemSelectText?: string;
    uniqueItemText?: string;
    customAddItemText?: string;
    addItemText?: (value: string) => string;
    removeItemIconText?: () => string;
    removeItemLabelText?: (value: string) => string;
    maxItemText?: (maxItemCount: number) => string;
    valueComparer?: (value1: any, value2: any) => boolean;
    classNames?: {
        containerOuter?: string[];
        containerInner?: string[];
        input?: string[];
        inputCloned?: string[];
        list?: string[];
        listItems?: string[];
        listSingle?: string[];
        listDropdown?: string[];
        item?: string[];
        itemSelectable?: string[];
        itemDisabled?: string[];
        itemChoice?: string[];
        description?: string[];
        placeholder?: string[];
        group?: string[];
        groupHeading?: string[];
        button?: string[];
        activeState?: string[];
        focusState?: string[];
        openState?: string[];
        disabledState?: string[];
        highlightedState?: string[];
        selectedState?: string[];
        flippedState?: string[];
        loadingState?: string[];
        notice?: string[];
        addChoice?: string[];
        noResults?: string[];
        noChoices?: string[];
    };
    fuseOptions?: any;
    labelId?: string;
    callbackOnInit?: () => void;
    callbackOnCreateTemplates?: (strToEl: (str: string) => HTMLElement, escapeForTemplate: (allowHTML: boolean, s: string) => string, getClassNames: (s: string[] | string) => string) => any;
    appendGroupInSearch?: boolean;
}

declare interface ChoicesDefaults {
    templates: ChoicesTemplates;
}

declare interface ChoicesTemplates {
    choice(options: any, choice: any, selectText: string, groupName?: string): HTMLDivElement;
    item(options: any, choice: any, removeItemButton: boolean): HTMLDivElement;
}

declare class Choices {
    constructor(element: HTMLElement | string, options?: ChoiceOptions);

    refresh();
    refresh(withEvents: boolean);
    refresh(withEvents: boolean, selectFirstOption: boolean);

    setChoiceByValue(value: string | string[]);

    static defaults: ChoicesDefaults;
}

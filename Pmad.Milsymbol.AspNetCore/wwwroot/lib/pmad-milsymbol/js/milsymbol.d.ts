declare namespace ms {
    function setStandard(standard: string): void;
    class Symbol {
        constructor(sidc: string, options: SymbolOptions);
        asSVG(): string;
        asCanvas(): HTMLCanvasElement;
    }
    interface SymbolOptions {
        size?: number;
        quantity?: number;
        staffComments?: string;
        additionalInformation?: string;
        direction?: number;
        type?: string;
        dtg?: string;
        location?: string;
    }
}
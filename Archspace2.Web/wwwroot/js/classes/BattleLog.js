class BattleLog {
    constructor(width, height) {
        this.width = width;
        this.height = height;
    }

    draw(element) {
        this.element = element;
        element.style.height = `${this.height}px`;
        element.style.width = `${this.width}px`;
        element.style.overflow = "auto";
        element.style.border = "1px solid #ccc";
    }

    clear() {
        this.element.innerHTML = "";
    }

    writeLine(line) {
        this.element.innerHTML += `<p style="margin: 0;">${line}</p>`;
    }

    writeLines(lines) {
        for (let i = 0; i < lines.length; i++) {
            this.writeLine(lines[i]);
        }
    }
}
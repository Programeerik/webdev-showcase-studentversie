class BoardCell extends HTMLElement {

    set connection(conn) {
        this._connection = conn;
    }

    constructor() {
        super();
        this.attachShadow({ mode: "open" });
        this.value = " ";
    }

    async connectedCallback() {
        this.renderHtml();
        await this.setListeners();
        this.shadowRoot.querySelector('.cell').addEventListener('click', this.addClick.bind(this));
    }

    renderHtml() {
        this.shadowRoot.innerHTML = `
            <style>
                .cell {
                    border:solid;#333333;1px;
                    width:50px;
                    height:50px;
                }
            </style>
            <div class="cell">${this.value}</div>
        `
    }

    async addClick() {

        if (this.value === " ") {
            const cellIndex = this.getAttribute('index');
            const groupName = this.getAttribute('group-name');
            const playerSymbol = this.getAttribute('player-symbol');

            await this._connection.invoke("SendMove", groupName, playerSymbol, parseInt(cellIndex));
            this.renderHtml();
        }
    }

    async setListeners() {
        this._connection.on("UpdateBoard", (playerSymbol, position) => {
            const targetCell = document.querySelector(`board-cell[index="${position}"]`);
            targetCell.setSymbol(playerSymbol);
        });

        this._connection.on("GameWon", (playerSymbol) => {
            document.querySelectorAll("board-cell").forEach(cell => {
                cell.innerHTML = " ";
            });

            const message = `<h1>${playerSymbol} heeft gewonnen!</h1>`;
            this.showGameEndPopUp(message);
        });

    }

    setSymbol(symbol) {
        this.value = symbol;
        this.renderHtml();
    }

    showGameEndPopUp(message) {
        const popup = document.createElement("div");
        popup.id = "GameEndPopUp";
        popup.innerHTML = message;
        popup.style = "z-index: 10;width: 50%;position: absolute;top: 20%;left: 25%;background: #333333;opacity: 0.7;color: white;text-align: center;padding: 1rem;"

        const button = document.createElement("button");
        button.onclick = () => {
            this._connection.stop();
            this.hideGameEndPopUp();
            location.href = location.href;
        };
        button.innerHTML = "Terug naar beginscherm.";
        popup.appendChild(button);
        document.querySelector("body").appendChild(popup);
    }

    hideGameEndPopUp() {
        document.querySelector("#GameEndPopUp").remove();
    }
}

customElements.define("board-cell", BoardCell);
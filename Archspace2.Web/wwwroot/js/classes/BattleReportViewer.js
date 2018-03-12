class BattleReportViewer {
    constructor() {
        this.data = new Object();

        this.turn = 0;
        this.runSimulation = false;
        this.view = new BattleView(500, 500, 10, 20);
    }

    get maxTurn() {
        return this.events[this.events.length - 1].turn;
    }

    load(data) {
        this.data = data;

        this.events = data.events;

        this.attacker = new Player(data.attacker);
        this.defender = new Player(data.defender);

        this.attackerFleets = [];
        for (let i = 0; i < data.attacker.fleets.length; i++) {
            this.attackerFleets.push(new Fleet(data.attacker.fleets[i],
                data.events.filter(x => x.type == "Movement" && x.fleetId == data.attacker.fleets[i].id),
                data.events.filter(x => x.type == "FleetDisabled" && x.disabledFleetId == data.attacker.fleets[i].id)
            ));
        }

        this.defenderFleets = [];
        for (let i = 0; i < data.defender.fleets.length; i++) {
            this.defenderFleets.push(new Fleet(data.defender.fleets[i],
                data.events.filter(x => x.type == "Movement" && x.fleetId == data.defender.fleets[i].id),
                data.events.filter(x => x.type == "FleetDisabled" && x.disabledFleetId == data.defender.fleets[i].id)
            ));
        }

        this.battlefield = data.battlefield;

        this.updateFleetPositions(0);
        this.updateTurnInfo();
    }

    runTurn(number = 1) {
        if (this.turn + number > this.maxTurn) {
            number = this.maxTurn - this.turn;
        }
        else if (this.turn + number < 0) {
            number = 0 - this.turn;
        }

        if (number > 0) {
            this.updateFleetPositions(this.turn + number);
        }
        else if (number < 0) {
            this.updateFleetPositions(this.turn + number);
        }
        else {

        }

        this.turn += number;

        this.updateTurnInfo();
    }

    updateTurnInfo() {
        this.element.querySelector("#br-info-turn").textContent = this.turn + "/" + this.maxTurn;
    }

    updateFleetPositions(turn) {
        this.view.clearFleets();

        this.attackerFleets.forEach(x => {
            x.updateTurn(turn);
            if (x.isCapital) {
                this.view.drawFleet(x, "red");
            }
            else {
                this.view.drawFleet(x, "grey");
            }
        });

        this.defenderFleets.forEach(x => {
            x.updateTurn(turn);
            if (x.isCapital) {
                this.view.drawFleet(x, "red");
            }
            else {
                this.view.drawFleet(x, "grey");
            }
        });
    }

    draw(element) {
        this.element = element;
        element.style.position = "relative";

        element.innerHTML = "<div id='br'><div id='br-view'></div><div id='br-controls'><button id='br-controls-fr'>FR</button><button id='br-controls-r'>R</button><button id='br-controls-toggle'>P</button><button id='br-controls-f'>F</button><button id='br-controls-ff'>FF</button></div><div id='br-info'><p id='br-info-turn'></p></div></div>";
        this.view.draw(element.querySelector("#br-view"));

        element.querySelector("#br-controls-fr").addEventListener("click", () => this.fastRewind());
        element.querySelector("#br-controls-r").addEventListener("click", () => this.rewind());
        element.querySelector("#br-controls-toggle").addEventListener("click", async () => await this.toggleAsync());
        element.querySelector("#br-controls-f").addEventListener("click", () => this.forward());
        element.querySelector("#br-controls-ff").addEventListener("click", () => this.fastForward());
    }

    async toggleAsync() {
        this.runSimulation = !this.runSimulation;

        await this.startSimulationAsync();
    }

    async startSimulationAsync() {
        while (this.runSimulation && this.turn < this.maxTurn) {
            this.forward();
            await sleep(50);
        }
    }

    fastRewind() {
        this.runTurn(-10);
    }

    rewind() {
        this.runTurn(-1);
    }

    forward() {
        this.runTurn(1);
    }

    fastForward() {
        this.runTurn(10);
    }
}
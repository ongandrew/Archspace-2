class BattleReportViewer {
    constructor() {
        this.data = new Object();

        this.turn = 0;
        this.runSimulation = false;
        this.view = new BattleView(500, 500, 10, 20);
        this.log = new BattleLog();
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

    get fleets() {
        return this.attackerFleets.concat(this.defenderFleets);
    }

    get fireEvents() {
        return this.events.filter(x => x.type == "Fire");
    }

    get hitEvents() {
        return this.events.filter(x => x.type == "Hit");
    }

    runTurn(number = 1) {
        let previousTurn = this.turn;

        if (this.turn + number > this.maxTurn) {
            number = this.maxTurn - this.turn;
        }
        else if (this.turn + number < 0) {
            number = 0 - this.turn;
        }

        let nextTurn = this.turn + number;
        
        if (number > 0) {
            this.updateFleetPositions(nextTurn);
            this.renderFireEvents(nextTurn);
        }
        else if (number < 0) {
            this.updateFleetPositions(nextTurn);
            this.renderFireEvents(nextTurn);
        }
        else {
            // Left intentionally empty
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

    renderFireEvents(turn) {
        this.view.clearFireEvents();

        // Need to remove hardcoding of max age = 30;
        // Unfortunately JS doesn't support nice class static constants yet. Revisit when ES7 is published.
        let fireEvents = this.fireEvents.filter(x => x.turn >= turn - 30 && x.turn <= turn);

        for (let i = 0; i < fireEvents.length; i++) {
            let firingFleet = this.fleets.filter(x => fireEvents[i].firingFleetId == x.id)[0];
            let targetFleet = this.fleets.filter(x => fireEvents[i].targetFleetId == x.id)[0];
            let weaponType = fireEvents[i].weaponType;
            let eventAge = turn - fireEvents[i].turn;

            this.view.drawFireEvent(firingFleet, targetFleet, weaponType, eventAge);
        }
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
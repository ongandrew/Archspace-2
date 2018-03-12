class Fleet {
    constructor(fleetData, movementEventData, disabledEventData) {
        this.id = fleetData.id;
        this.name = fleetData.name;
        this.isCapital = fleetData.isCapital;

        this.turn = 0;

        this.movementEventData = movementEventData;
        this.disabledEventData = disabledEventData;
    }

    get disabledTurn() {
        if (this.isDisabled) {
            return this.getDisabledEventDataOnTurn(this.turn).Turn;
        }
    }

    getDisabledEventDataOnTurn(turn) {
        let disabledData = this.disabledEventData.filter(x => x.turn <= turn);

        if (disabledData.length > 0) {
            return disabledData[0];
        }
        else {
            return null;
        }
    }

    getMovementEventDataOnTurn(turn) {
        let movementData = this.movementEventData.filter(x => x.turn <= turn);

        let result = movementData[0];
        let max = movementData[0].turn;

        for (let i = 0; i < movementData.length; i++) {
            if (movementData[i].turn > max) {
                max = movementData[i].turn;
                result = movementData[i];
            }
        }

        return result;
    }

    getShadow(turn) {
        let movementData = this.getMovementEventDataOnTurn(turn);

        let result = new Object();
        result.x = movementData.x;
        result.y = movementData.y;
        result.direction = movementData.direction;
    }

    updateTurn(turn) {
        let movementEventData = this.getMovementEventDataOnTurn(turn);

        this.x = movementEventData.x;
        this.y = movementEventData.y;
        this.direction = movementEventData.direction;
        this.remainingShips = movementEventData.remainingShips;
        this.command = movementEventData.command;
        this.status = movementEventData.status;
        this.substatus = movementEventData.substatus;

        let disabledEventData = this.getDisabledEventDataOnTurn(turn);

        if (disabledEventData == null) {
            this.isDisabled = false;
        }
        else {
            this.isDisabled = true;
        }
    }
}
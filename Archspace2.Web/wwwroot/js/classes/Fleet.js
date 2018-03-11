class Fleet {
    constructor(fleetData, movementEventData, disabledEventData) {
        this.id = fleetData.Id;
        this.name = fleetData.Name;
        this.isCapital = fleetData.IsCapital;

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
        let disabledData = this.disabledEventData.filter(x => x.Turn <= turn);

        if (disabledData.length > 0) {
            return disabledData[0];
        }
        else {
            return null;
        }
    }

    getMovementEventDataOnTurn(turn) {
        let movementData = this.movementEventData.filter(x => x.Turn <= turn);

        let result = movementData[0];
        let max = movementData[0].Turn;

        for (let i = 0; i < movementData.length; i++) {
            if (movementData[i].Turn > max) {
                max = movementData[i].Turn;
                result = movementData[i];
            }
        }

        return result;
    }

    getShadow(turn) {
        let movementData = this.getMovementEventDataOnTurn(turn);

        let result = new Object();
        result.x = movementData.X;
        result.y = movementData.Y;
        result.direction = movementData.Direction;
    }

    updateTurn(turn) {
        let movementEventData = this.getMovementEventDataOnTurn(turn);
        
        this.x = movementEventData.X;
        this.y = movementEventData.Y;
        this.direction = movementEventData.Direction;
        this.remainingShips = movementEventData.RemainingShips;
        this.command = movementEventData.Command;
        this.status = movementEventData.Status;
        this.substatus = movementEventData.Substatus;

        let disabledEventData = this.getDisabledEventDataOnTurn(turn);

        if (disabledEventData == null) {
            this.isDisabled = false;
        }
        else {
            this.isDisabled = true;
        }
    }
}
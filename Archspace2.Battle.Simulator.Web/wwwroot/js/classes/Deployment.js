class Deployment {
    constructor(point, angle, fleetId, fleetDisplayName, isCapitalFleet, command) {
        this.point = point;
        this.angle = angle;
        this.fleetId = fleetId;
        this.fleetDisplayName = fleetDisplayName;
        this.isCapitalFleet = isCapitalFleet;
        this.command = command;

        this.isBeingDragged = false;
    }

    get canvasFillStyle() {
        if (this.isCapitalFleet == true) {
            return "red";
        }
        else {
            return "grey";
        }
    }

    get isDraggable() {
        if (this.isCapitalFleet == true) {
            return false;
        }
        else {
            return true;
        }
    }

    calculateCanvasCentroid(scale, padding) {
        return (new Point((this.point.y - 2000) / scale, (this.point.x - 7000) / scale)).plus(new Point(padding, padding));
    }

    moveCanvasCentroid(scale, padding, canvasPoint) {
        let point = canvasPoint.minus(new Point(padding, padding));

        this.point.x = (point.y * scale) + 7000;
        this.point.y = (point.x * scale) + 2000;
    }

    calculateCanvasShapePoints(scale, padding, spacing) {
        let result = [];

        let canvasPoint = this.calculateCanvasCentroid(scale, padding);

        let point1 = new Point(canvasPoint.x - (spacing / 3), canvasPoint.y + (spacing / 2));
        let point2 = new Point(canvasPoint.x + (spacing / 3), canvasPoint.y + (spacing / 2));
        let point3 = new Point(canvasPoint.x, canvasPoint.y - (spacing / 2));

        result.push(point1.rotate(canvasPoint, this.angle - 180));
        result.push(point2.rotate(canvasPoint, this.angle - 180));
        result.push(point3.rotate(canvasPoint, this.angle - 180));

        return result;
    }

    checkBoundingBox(scale, padding, spacing, point) {
        let canvasPoint = this.calculateCanvasCentroid(scale, padding);

        let leftX = canvasPoint.x - (spacing / 2);
        let rightX = canvasPoint.x + (spacing / 2);
        let bottomY = canvasPoint.y - (spacing / 2);
        let topY = canvasPoint.y + (spacing / 2);

        if (point.x >= leftX && point.x <= rightX && point.y >= bottomY && point.y <= topY) {
            return true;
        }
        else {
            return false;
        }
    }
}
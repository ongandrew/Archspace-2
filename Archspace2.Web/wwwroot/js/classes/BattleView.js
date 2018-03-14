class BattleView
{
    constructor(width, height, padding, spacing)
    {
        this.height = height;
        this.width = width;
        this.padding = padding;
        this.spacing = spacing;
    }

    get scale()
    {
        let scaleH = 10000 / this.height;
        let scaleW = 10000 / this.width;

        if (scaleH != scaleW)
        {
            throw "Different scales in x and y directions not supported.";
        }
        else
        {
            return scaleH;
        }
    }

    draw(element)
    {
        this.element = element;
        element.style.position = "relative";
        element.innerHTML = "<canvas id='battle-viewer-grid' style='position: absolute; z-index: 0;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "\n<canvas id='battle-viewer-shadows' style='position: absolute; z-index: 1;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "\n<canvas id='battle-viewer-fire-events' style='position: absolute; z-index: 2;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "\n<canvas id='battle-viewer-fleets' style='position: absolute; z-index: 3;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "\n<canvas id='battle-viewer-static' style='position: static; z-index: -1;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";

        let canvas = element.querySelector("#battle-viewer-grid");
        let context = canvas.getContext("2d");

        context.strokeStyle = "grey";

        for (let x = 0; x <= this.width / this.spacing; x++)
        {
            context.moveTo(this.padding + (x * this.spacing), this.padding);
            context.lineTo(this.padding + (x * this.spacing), this.height + this.padding);
        }

        for (let y = 0; y <= this.height / this.spacing; y++)
        {
            context.moveTo(this.padding, this.padding + (y * this.spacing));
            context.lineTo(this.width + this.padding, this.padding + (y * this.spacing));
        }

        context.stroke();
    }

    clearShadows() {
        let element = this.element;
        let shadowLayer = element.querySelector("#battle-viewer-shadows");

        let context = shadowLayer.getContext("2d");

        context.clearRect(0, 0, shadowLayer.width, shadowLayer.height);
    }

    clearFleets() {
        let element = this.element;
        let fleetLayer = element.querySelector("#battle-viewer-fleets");

        let context = fleetLayer.getContext("2d");

        context.clearRect(0, 0, fleetLayer.width, fleetLayer.height);
    }

    clearFireEvents() {
        let element = this.element;
        let fireLayer = element.querySelector("#battle-viewer-fire-events");

        let context = fireLayer.getContext("2d");

        context.clearRect(0, 0, fireLayer.width, fireLayer.height);
    }

    drawFleet(fleet, style)
    {
        let element = this.element;
        let fleetLayer = element.querySelector("#battle-viewer-fleets");

        let context = fleetLayer.getContext("2d");

        let points = this.calculateCanvasShapePoints(fleet);

        if (!fleet.isDisabled) {
            context.fillStyle = style;
        }
        else {
            context.strokeStyle = style;
        }
        
        context.beginPath();
        for (let j = 0; j < points.length; j++)
        {
            if (j == 0)
            {
                context.moveTo(points[j].x, points[j].y);
            }
            else
            {
                context.lineTo(points[j].x, points[j].y);
            }
        }

        if (!fleet.isDisabled) {
            context.fill();
        }
        else {
            context.closePath();
            context.stroke();
        }
    }

    drawFireEvent(firingFleet, targetFleet, weaponType, eventAge) {
        let element = this.element;
        let fireLayer = element.querySelector("#battle-viewer-fire-events");

        let context = fireLayer.getContext("2d");

        let sourcePoint = this.calculateCentroid(firingFleet);
        let targetPoint = this.calculateCentroid(targetFleet);

        context.strokeStyle = this.calculateFireEventStyle(weaponType, eventAge);

        context.beginPath();

        context.moveTo(sourcePoint.x, sourcePoint.y);
        context.lineTo(targetPoint.x, targetPoint.y);

        context.closePath();
        context.stroke();
    }

    // style should be calculated here not passed in
    drawShadows(shadow, style) {
        let element = this.element;
        let shadowLayer = element.querySelector("#battle-viewer-shadows");

        let context = shadowLayer.getContext("2d");

        let points = this.calculateCanvasShapePoints(fleet);

        context.fillStyle = style;
        context.beginPath();
        for (let j = 0; j < points.length; j++) {
            if (j == 0) {
                context.moveTo(points[j].x, points[j].y);
            }
            else {
                context.lineTo(points[j].x, points[j].y);
            }
        }

        context.fill();
    }

    calculateCentroid(fleet)
    {
        return new Point(fleet.x / this.scale, fleet.y / this.scale).plus(new Point(this.padding, this.padding));
    }

    calculateCanvasShapePoints(fleet)
    {
        let result = [];

        let canvasPoint = this.calculateCentroid(fleet);

        let point1 = new Point(canvasPoint.x - (this.spacing / 3), canvasPoint.y + (this.spacing / 2));
        let point2 = new Point(canvasPoint.x + (this.spacing / 3), canvasPoint.y + (this.spacing / 2));
        let point3 = new Point(canvasPoint.x, canvasPoint.y - (this.spacing / 2));

        result.push(point1.rotate(canvasPoint, fleet.direction - 90));
        result.push(point2.rotate(canvasPoint, fleet.direction - 90));
        result.push(point3.rotate(canvasPoint, fleet.direction - 90));

        return result;
    }

    calculateFireEventStyle(weaponType, eventAge) {
        let result = "rgba(";

        switch (weaponType) {
            case "Projectile":
                result += "0,255,0,";
                break;
            case "Beam":
                result += "0,0,255,";
            case "Missile":
            default:
                result += "255,0,0,";
                break;
        }

        // 30 should be defined somewhere, but JS... and classes... sigh, let's wait for ES7 (ie never)
        let age = eventAge;
        if (eventAge > 30) {
            eventAge = 30;
        }
        let scaledAge = age / 30;
        let aValue = 1 - scaledAge;

        result += aValue;
        result += ")";

        return result;
    }
}
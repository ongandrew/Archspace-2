class Point {
    constructor(x, y) {
        this.x = x;
        this.y = y;
    }

    plus(point) {
        return new Point(this.x + point.x, this.y + point.y);
    }

    minus(point) {
        return new Point(this.x - point.x, this.y - point.y);
    }

    rotate(point, angle) {
        let translated = this.minus(point);
        let theta = Math.PI * angle / 180;
        let result = new Point(0, 0);
        result.x = (Math.cos(theta) * translated.x) + (Math.sin(theta) * translated.y);
        result.y = (-1.0 * Math.sin(theta) * translated.x) + (Math.cos(theta) * translated.y);

        result = result.plus(point);

        return result;
    }

    distance(point) {
        return Math.sqrt(Math.pow(this.x - point.x, 2) + Math.pow(this.y - point.y, 2));
    }
}

class DefensePlan {
    constructor(width, height, padding, spacing) {
        this.height = height;
        this.width = width;
        this.padding = padding;
        this.spacing = spacing;

        this.deployments = [];
        this.gridPoints = [];

        for (let x = 0; x <= this.width / this.spacing; x++) {
            for (let y = 0; y <= this.height / this.spacing; y++) {
                this.gridPoints.push(new Point((x * spacing) + padding, (y * spacing) + padding));
            }
        }
    }

    get scale() {
        let scaleH = 2000 / this.height;
        let scaleW = 6000 / this.width;

        if (scaleH != scaleW) {
            throw "Different scales in x and y directions not supported.";
        }
        else {
            return scaleH;
        }
    }

    addDeployment(deployment) {
        this.deployments.push(deployment); 
    }

    draw(element) {
        this.element = element;
        element.style.position = "relative";
        element.innerHTML = "<canvas id='defense-plan-grid' style='position: absolute; z-index: 0;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "\n<canvas id='defense-plan-fleets' style='position: absolute; z-index: 1;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "\n<canvas id='defense-plan-static' style='position: static; z-index: -1;' width='" + (this.width + (2 * this.padding)) + "' height='" + (this.height + (2 * this.padding)) + "' </canvas>";
        element.innerHTML += "<table><tr><td>Fleet</td><td id='fleet-name'></td></tr><tr><td>Command</td><td id='fleet-command'></td></tr></table>";
        element.innerHTML += "<div id='fleet-id' style='display: none'></div>";

        let canvas = element.querySelector("#defense-plan-grid");
        let context = canvas.getContext("2d");

        context.strokeStyle = "white";

        for (let x = 0; x <= this.width / this.spacing; x++) {
            context.moveTo(this.padding + (x * this.spacing), this.padding);
            context.lineTo(this.padding + (x * this.spacing), this.height + this.padding);
        }

        for (let y = 0; y <= this.height / this.spacing; y++) {
            context.moveTo(this.padding, this.padding + (y * this.spacing));
            context.lineTo(this.width + this.padding, this.padding + (y * this.spacing));
        }

        context.stroke();

        this.updateFleets();

        this.addMouseEvents();
    }

    updateFleets() {
        let element = this.element;
        let fleetLayer = element.querySelector("#defense-plan-fleets");
        let deployments = this.deployments;

        let context = fleetLayer.getContext("2d");

        context.clearRect(0, 0, fleetLayer.width, fleetLayer.height);

        for (let i = 0; i < deployments.length; i++) {
            let points = deployments[i].calculateCanvasShapePoints(this.scale, this.padding, this.spacing);

            context.fillStyle = deployments[i].canvasFillStyle;
            context.beginPath();
            for (let j = 0; j < points.length; j++) {
                if (j == 0) {
                    context.moveTo( points[j].x, points[j].y);
                }
                else {
                    context.lineTo(points[j].x, points[j].y);
                }
            }

            context.fill();
        }
    }

    addMouseEvents() {
        let element = this.element;
        let fleetLayer = element.querySelector("#defense-plan-fleets");

        let offsetX = fleetLayer.getBoundingClientRect().left;
        let offsetY = fleetLayer.getBoundingClientRect().top;

        fleetLayer.onmousedown = (e) => {
            e.preventDefault();
            e.stopPropagation();

            let canvasPoint = new Point(e.clientX - offsetX, e.clientY - offsetY);

            let deployment = this.getClosestNonCapitalDeployment(canvasPoint);

            if (deployment.checkBoundingBox(this.scale, this.padding, this.spacing, canvasPoint) == true) {
                if (deployment.isDraggable == true) {
                    this.isDragging = true;
                    deployment.isBeingDragged = true;
                }

                this.showFleetInfo(deployment);
            }
            else {
                deployment = this.getClosestDeployment(canvasPoint);

                if (deployment.checkBoundingBox(this.scale, this.padding, this.spacing, canvasPoint) == true) {
                    this.showFleetInfo(deployment);
                }
            }
        };

        fleetLayer.onmouseup = (e) => {
            e.preventDefault();
            e.stopPropagation();

            let canvasPoint = new Point(e.clientX - offsetX, e.clientY - offsetY);

            if (this.isDragging == true) {
                let deployment = this.getDeploymentBeingDragged();

                deployment.moveCanvasCentroid(this.scale, this.padding, this.getClosestGridPoint(canvasPoint));

                this.updateFleets();

                deployment.isBeingDragged = false;
                this.isDragging = false;
            }
        }

        fleetLayer.onmousemove = (e) => {
            e.preventDefault();
            e.stopPropagation();

            let canvasPoint = new Point(e.clientX - offsetX, e.clientY - offsetY);

            if (this.isDragging == true) {
                let deployment = this.getDeploymentBeingDragged();

                deployment.moveCanvasCentroid(this.scale, this.padding, canvasPoint);

                this.updateFleets();
            }
        }
    }

    getCapitalDeployment() {
        let deployments = this.deployments;

        for (let i = 0; i < deployments.length; i++) {
            if (deployments[i].isCapitalFleet) {
                return deployments[i];
            }
        }

        return null;
    }

    getClosestDeployment(point) {
        let deployments = this.deployments;
        let closestDeployment = deployments[0];
        let smallestDistance = deployments[0].calculateCanvasCentroid(this.scale, this.padding).distance(point);

        for (let i = 1; i < deployments.length; i++) {
            let distance = deployments[i].calculateCanvasCentroid(this.scale, this.padding).distance(point);
            if (distance < smallestDistance) {
                smallestDistance = distance;
                closestDeployment = deployments[i];
            }
        }

        return closestDeployment;
    }

    getClosestNonCapitalDeployment(point) {
        let deployments = this.deployments;
        let closestDeployment = null;
        let smallestDistance = 10000;

        for (let i = 0; i < deployments.length; i++) {
            let distance = deployments[i].calculateCanvasCentroid(this.scale, this.padding).distance(point);
            if (distance < smallestDistance && deployments[i].isCapitalFleet == false) {
                smallestDistance = distance;
                closestDeployment = deployments[i];
            }
        }

        return closestDeployment;
    }

    getDeploymentBeingDragged() {
        let deployments = this.deployments;

        for (let i = 0; i < deployments.length; i++) {
            if (deployments[i].isBeingDragged == true) {
                return deployments[i];
            }
        }

        return null;
    }

    getClosestGridPoint(point) {
        let gridPoints = this.gridPoints;

        let closestPoint = gridPoints[0];
        let smallestDistance = gridPoints[0].distance(point);

        for (let i = 1; i < gridPoints.length; i++) {
            let distance = gridPoints[i].distance(point);

            if (distance < smallestDistance) {
                smallestDistance = distance;
                closestPoint = gridPoints[i];
            }
        }

        return closestPoint;
    }

    showFleetInfo(deployment) {
        let element = this.element;
        let id = element.querySelector("#fleet-id");
        let name = element.querySelector("#fleet-name");
        let command = element.querySelector("#fleet-command");

        id.textContent = parseInt(deployment.fleetId);
        name.textContent = deployment.fleetDisplayName;

        let string = "<select>";

        let keys = Object.keys(commands);

        for (let i = 0; i < keys.length; i++) {
            string += "<option value='" + keys[i] + "'>" + commands[keys[i]] + "</option>";
        }

        string += "</select>";

        command.innerHTML = string;

        let selectElement = command.querySelector("select");

        for (let i = 0; i < selectElement.options.length; i++) {
            if (selectElement.options[i].value == deployment.command) {
                selectElement.selectedIndex = i;
                break;
            }
        }

        selectElement.onchange = () => {
            deployment.command = selectElement.options[selectElement.selectedIndex].value;
        };
    }
}

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

async function getFleetCommandsAsync() {
    let request = new Request("/fleet/commands", {
        method: "GET",
        mode: "same-origin"
    });

    return await fetch(request).then(handleErrorCodes).then(returnBodyAsJson);
}

async function saveDefensePlanAsync() {
    let body = new Object();
    body.deployments = defensePlan.deployments;

    for (let i = 0; i < body.deployments.length; i++) {
        body.deployments[i].x = body.deployments[i].point.x;
        body.deployments[i].y = body.deployments[i].point.y;
    }

    let request = new Request("/war/save_defense_Plan", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow",
        body: JSON.stringify(body),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    try {
        await fetch(request).then(handleErrorCodes).then((response) => {
            if (response.ok) {
                location.reload();
            }
        });
    }
    catch (error) {
        console.log(error);
    }
}
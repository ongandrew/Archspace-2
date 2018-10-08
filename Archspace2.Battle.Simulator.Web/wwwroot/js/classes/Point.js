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

    reflectPoint(point) {
        return new Point((2 * point.x) - this.x, (2 * point.y) - this.y);
    }

    reflectLine(line) {
        let d = (this.x + ((this.y + line.intercept) * line.gradient)) / (1 + Math.pow(line.gradient, 2));
        return new Point((2 * d) - this.x, (2 * d * line.gradient) - this.y + (2 * line.intercept));
    }

    distance(point) {
        return Math.sqrt(Math.pow(this.x - point.x, 2) + Math.pow(this.y - point.y, 2));
    }
}
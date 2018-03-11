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
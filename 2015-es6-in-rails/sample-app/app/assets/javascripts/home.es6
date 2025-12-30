$(() => {
    console.log('hello from jQuery onload!');
});


class GeneratorExample {
    constructor() {}

    getStuff () {
        return [1,2,3];
    }
}

let obj = new GeneratorExample();
let sum = obj.getStuff().reduce((prev, current) => prev + current);
console.log(`The sum is ${sum}`);


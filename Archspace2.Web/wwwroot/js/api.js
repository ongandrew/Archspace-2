async function createUniverseAsync() {
    await fetch("/game/universe", {
        method: "POST"
    }).then(handleErrorCodes);
}

async function startGameAsync() {
    await fetch("/game/start", {
        method: "POST"
    }).then(handleErrorCodes);
}
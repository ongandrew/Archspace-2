async function createUniverseAsync() {
    var request = new Request("/game/universe", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow"
    });

    await fetch(request).then(handleErrorCodes).then(followRedirects);
}

async function startGameAsync() {
    var request = new Request("/game/start", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow"
    });

    await fetch(request).then(handleErrorCodes).then(followRedirects);
}
function populateCouncils() {
    let councilElement = document.querySelector("#councils");

    let optionString = "";
    for (let i = 0; i < councils.length; i++) {
        optionString += "<option value=" + councils[i].id + ">" + councils[i].name + "</option>";
    }

    councilElement.innerHTML = optionString;

    councilElement.onchange = () => {
        let index = councilElement.options.selectedIndex;

        let players = councils[index].players;

        let playerElement = document.querySelector("#players");
        let playerOptionString = "";

        for (let i = 0; i < players.length; i++) {
            playerOptionString += "<option value=" + players[i].id + ">" + players[i].name + "</option>";
        }

        playerElement.innerHTML = playerOptionString;
    };

    councilElement.onchange();
}

async function inspectPlayerAsync() {
    let id = parseInt(document.querySelector("#players")[document.querySelector("#players").selectedIndex].value);

    var request = new Request("/archspace/player_info/" + id, {
        method: "GET",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow"
    });

    await fetch(request).then(handleErrorCodes).then(goToLocation);
}
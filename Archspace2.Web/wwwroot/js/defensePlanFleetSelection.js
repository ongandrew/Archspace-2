function getFleetSelection() {
    let capitalFleetId = parseInt(document.querySelector("input[name='capital']:checked").value);
    let fleets = document.querySelectorAll("input[name='include']:checked");

    let requestBody = new Object();
    requestBody.capitalFleetId = capitalFleetId;
    requestBody.fleets = [];
    for (let i = 0; i < fleets.length; i++) {
        requestBody.fleets.push(parseInt(fleets[i].value));
    }

    return requestBody;
}

function onSelectCapital() {
    let capitalFleetId = parseInt(document.querySelector("input[name='capital']:checked").value);
    let fleets = document.querySelectorAll("input[name='include']");

    for (let i = 0; i < fleets.length; i++) {
        if (parseInt(fleets[i].value) == capitalFleetId) {
            fleets[i].checked = true;
            fleets[i].disabled = true;
        }
        else {
            fleets[i].disabled = false;
        }
    }
}

async function selectFleetsForPlanAsync() {
    let body = getFleetSelection();

    let request = new Request("/war/select_defense_plan_fleets", {
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
        await fetch(request).then(handleErrorCodes).then(followRedirects);
    }
    catch (error) {
        console.log(error);
    }
}
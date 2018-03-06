function populateShipSelection() {
    let list = $("#ship-list");
    list.html("");

    if (Object.keys(availableShipInfo).length == 0) {
        list.html("<option value=0>None</option>");
    }
    else {
        let optionString = "";

        for (let shipInfo in availableShipInfo) {
            optionString += "<option value=" + shipInfo + ">" + availableShipInfo[shipInfo].name + "</option>";
        }

        list.html(optionString);

        updateAvailableCount();
    }
}

function updateAvailableCount() {
    let selection = $("#ship-list");
    let id = parseInt(selection[0][selection[0].selectedIndex].value);

    let span = $("#available");
    span.html(availableShipInfo[id].available);
}

function getNewFleetParameters() {
    let result = new Object();

    let order = parseInt(document.querySelector("#order").value);
    let name = document.querySelector("#name").value;
    let number = parseInt(document.querySelector("#number").value);

    let selection = document.querySelector("#ship-list");
    let shipDesignId = parseInt(selection.options[selection.selectedIndex].value);

    let admiralChecked = document.querySelector("input[name='admiralId']:checked");
    let admiralId = 0;
    if (admiralChecked != null) {
        admiralId = parseInt(admiralChecked.value);
    }

    result.order = order;
    result.name = name;
    result.shipDesignId = shipDesignId;
    result.admiralId = admiralId;
    result.number = number;

    return result;
}

async function formNewFleetAsync() {
    var form = getNewFleetParameters();

    var request = new Request("/fleet/form_new_fleet", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow",
        body: JSON.stringify(form),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    try {
        await fetch(request).then(handleErrorCodes).then(refreshLocation);
    }
    catch (error) {
        console.log(error);
    }
}
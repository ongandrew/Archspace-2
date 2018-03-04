function populateShipSelection() {
    let list = $("#ship-list");
    list.html("");

    if (Object.keys(availableShipInfo).length == 0) {
        list.html("<option>None</option>");
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
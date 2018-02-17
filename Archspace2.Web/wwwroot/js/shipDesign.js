function loadShipClassOptions() {
    var weaponSlots = shipClassInfo[$("#ship-class").val()].weaponSlots;
    var deviceSlots = shipClassInfo[$("#ship-class").val()].deviceSlots;
    var spacePerSlot = shipClassInfo[$("#ship-class").val()].space / weaponSlots;

    var weaponElement = $("#weapons");
    var weaponOptions = "";

    for (var i = 0; i < weaponSlots; i++) {
        weaponOptions += "<select>";
        for (var weapon in weaponInfo) {
            weaponOptions += "<option value=" + weapon + ">" + weaponInfo[weapon].name + " x" + (spacePerSlot / weaponInfo[weapon].space).toFixed(0) + "</option>";
        }
        weaponOptions += "</select>";
    }

    weaponElement.html(weaponOptions);

    var deviceElement = $("#devices");
    var deviceOptions = "";

    if (deviceSlots < 1) {
        deviceElement.attr("hidden", "");
    }
    else {
        deviceElement.removeAttr("hidden");
        for (let i = 0; i < deviceSlots; i++) {
            deviceOptions += "<select>";
            for (let device in deviceInfo) {
                deviceOptions += "<option value=" + device + ">" + deviceInfo[device].name + "</option>";
            }
            deviceOptions += "</select>";
        }
    }

    deviceElement.html(deviceOptions);
}

function getCurrentDesign() {
    var design = new Object();

    design.name = $("#name").val();

    design.class = parseInt($("#ship-class").val());

    design.computer = parseInt($("#computer").val());
    design.armor = parseInt($("#armor").val());
    design.engine = parseInt($("#engine").val());
    design.shield = parseInt($("#shield").val());

    design.weapons = [];
    design.devices = [];

    var weaponSelections = $("#weapons select");
    for (let i = 0; i < weaponSelections.length; i++) {
        let index = weaponSelections[i].selectedIndex;
        design.weapons.push(parseInt(weaponSelections[i][index].value));
    }

    var deviceSelections = $("#devices select");
    for (let i = 0; i < deviceSelections.length; i++) {
        let index = deviceSelections[i].selectedIndex;
        if (deviceSelections[i][index].value !=  0) {
            design.devices.push(parseInt(deviceSelections[i][index].value));
        }
    }

    return design;
}

async function submitDesignAsync() {
    var design = getCurrentDesign();

    var request = new Request("/ship_design/create", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow",
        body: JSON.stringify(design),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    await fetch(request).then(handleErrorCodesAndThenFollowRedirects);
}
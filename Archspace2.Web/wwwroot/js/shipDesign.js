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
        weaponOptions += "</select>"
    }

    weaponElement.html(weaponOptions);

    var deviceElement = $("#devices");
    var deviceOptions = "";

    if (deviceSlots < 1) {
        deviceElement.attr("hidden", "");
    }
    else {
        deviceElement.removeAttr("hidden");
        for (var i = 0; i < deviceSlots; i++) {
            deviceOptions += "<select>"
            for (var device in deviceInfo) {
                deviceOptions += "<option value=" + device + ">" + deviceInfo[device].name + "</option>";
            }
            deviceOptions += "</select>"
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
    design.shields = parseInt($("#shields").val());

    design.weapons = [];
    design.devices = [];

    var weaponSelections = $("#weapons select");
    for (var i = 0; i < weaponSelections.length; i++) {
        var index = weaponSelections[i].selectedIndex;
        design.weapons.push(parseInt(weaponSelections[i][index].value));
    }

    var deviceSelections = $("#devices select");
    for (var i = 0; i < deviceSelections.length; i++) {
        var index = deviceSelections[i].selectedIndex;
        if (deviceSelections[i][index].value != 0) {
            design.devices.push(parseInt(deviceSelections[i][index].value));
        }
    }

    return design;
}
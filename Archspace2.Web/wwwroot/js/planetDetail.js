function increaseDistributionRatio(buildingType) {
    var factory = getDistributionRatioAsValue($("#ratio-factory").html());
    var researchLab = getDistributionRatioAsValue($("#ratio-research-lab").html());
    var militaryBase = getDistributionRatioAsValue($("#ratio-military-base").html());

    var newFactory = factory;
    var newResearchLab = researchLab;
    var newMilitaryBase = militaryBase;

    if (buildingType == "factory") {
        if (factory + 10 <= 100) {
            newFactory = factory + 10;

            if (researchLab > militaryBase && researchLab - 10 >= 0) {
                newResearchLab = researchLab - 10;
            }
            else {
                newMilitaryBase = militaryBase - 10;
            }
        }
    }
    else if (buildingType == "research-lab") {
        if (researchLab + 10 <= 100) {
            newResearchLab = researchLab + 10;

            if (factory > militaryBase && factory - 10 >= 0) {
                newFactory = factory - 10;
            }
            else {
                newMilitaryBase = militaryBase - 10;
            }
        }
    }
    else if (buildingType == "military-base") {
        if (militaryBase + 10 <= 100) {
            newMilitaryBase = militaryBase + 10;

            if (factory > researchLab && factory - 10 >= 0) {
                newFactory = factory - 10;
            }
            else {
                newResearchLab = researchLab - 10;
            }
        }
    }

    updateRatios(newFactory, newResearchLab, newMilitaryBase);
}

function decreaseDistributionRatio(buildingType) {
    var factory = getDistributionRatioAsValue($("#ratio-factory").html());
    var researchLab = getDistributionRatioAsValue($("#ratio-research-lab").html());
    var militaryBase = getDistributionRatioAsValue($("#ratio-military-base").html());
}

function getDistributionRatioAsValue(html) {
    var result = html.trim();
    var lastChar = result[result.length - 1];
    if (lastChar != '%') {
        throw "Invalid distribution ratio!";
    }
    else {
        result = result.substring(0, result.length - 1);
        var integerValue = parseInt(result);

        if (integerValue == NaN) {
            throw "Invalid distribution ratio!";
        }
        else {
            return integerValue;
        }
    }
}

function updateRatios(factory, researchLab, militaryBase) {
    $("#ratio-factory").html(factory + "%");
    $("#ratio-research-lab").html(researchLab + "%");
    $("#ratio-military-base").html(militaryBase + "%");

    renderRatioControls("factory", factory);
    renderRatioControls("research-lab", researchLab);
    renderRatioControls("military-base", militaryBase);
}

function renderRatioControls(buildingType, amount) {
    var boxes = $("#ratio-" + buildingType + "-controls img");
    
    for (var i = 0; i < boxes.length; i++) {
        if (amount / 10 > i) {
            boxes[i].setAttribute("src", "/images/planets/planet_check.gif");
        }
        else {
            boxes[i].setAttribute("src", "/images/planets/planet_box.gif");
        }
    }
}

async function changeDistributionRatioAsync() {
    var factory = getDistributionRatioAsValue($("#ratio-factory").html());
    var researchLab = getDistributionRatioAsValue($("#ratio-research-lab").html());
    var militaryBase = getDistributionRatioAsValue($("#ratio-military-base").html());

    var bodyData = new Object();
    bodyData.factory = factory;
    bodyData.researchLab = researchLab;
    bodyData.militaryBase = militaryBase;

    var request = new Request("/planet/" + id + "/change_ratio", {
        method: "POST",
        body: JSON.stringify(bodyData),
        mode: "same-origin",
        credentials: "include",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    await fetch(request).then(handleErrorCodes);
}
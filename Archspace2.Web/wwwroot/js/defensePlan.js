async function getFleetCommandsAsync() {
    let request = new Request("/fleet/commands", {
        method: "GET",
        mode: "same-origin"
    });

    return await fetch(request).then(handleErrorCodes).then(returnBodyAsJson);
}

async function saveDefensePlanAsync() {
    let body = new Object();
    body.deployments = defensePlan.deployments;

    for (let i = 0; i < body.deployments.length; i++) {
        body.deployments[i].x = body.deployments[i].point.x;
        body.deployments[i].y = body.deployments[i].point.y;
    }

    let request = new Request("/war/save_defense_Plan", {
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
        await fetch(request).then(handleErrorCodes).then((response) => {
            if (response.ok) {
                location.reload();
            }
        });
    }
    catch (error) {
        console.log(error);
    }
}
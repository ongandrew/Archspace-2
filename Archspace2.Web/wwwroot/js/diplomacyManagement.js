function getCurrentAction() {
    let result = new Object();

    result.action = document.querySelector("#action")[document.querySelector("#action").selectedIndex].value;

    let checked = document.querySelectorAll("input[name='ids']:checked");

    result.ids = [];
    for (let i = 0; i < checked.length; i++) {
        result.ids.push(parseInt(checked[i].value));
    }

    return result;
}

async function getMassActionsAsync() {
    let request = new Request("/diplomacy/mass_actions", {
        method: "GET",
        mode: "same-origin"
    });

    return await fetch(request).then(handleErrorCodes).then(returnBodyAsJson);
}

async function performMassActionsAsync() {
    let body = getCurrentAction();

    let request = new Request("/diplomacy/execute_mass_action", {
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
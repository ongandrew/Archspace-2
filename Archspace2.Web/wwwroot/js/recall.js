async function recallFleetsAsync() {
    let body = new Object();

    body.ids = [];

    let checkedBoxes = document.querySelectorAll("input[value][type='checkbox'][name='ids']:checked");
    for (let i = 0; i < checkedBoxes.length; i++) {
        body.ids.push(parseInt(checkedBoxes[i].value));
    }

    var request = new Request("/fleet/recall_fleets", {
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
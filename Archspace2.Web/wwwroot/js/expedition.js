async function sendExpeditionAsync() {
    let element = document.querySelector("input[name='id']:checked");
    if (element != null) {
        let fleetId = parseInt(element.value);

        var request = new Request("/fleet/" + fleetId + "/expedition", {
            method: "POST",
            mode: "same-origin",
            credentials: "include",
            redirect: "follow"
        });

        try {
            await fetch(request).then(handleErrorCodes).then(refreshLocation);
        }
        catch (error) {
            console.log(error);
        }
    }
}
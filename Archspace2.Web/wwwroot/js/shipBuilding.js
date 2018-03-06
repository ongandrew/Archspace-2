async function cancelBuildOrderAsync(id) {
    var request = new Request("/ship_design/" + id + "/cancel", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow"
    });

    await fetch(request).then(handleErrorCodes).then(followRedirects);
}
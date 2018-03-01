async function scrapShipsAsync() {
    var form = getShipsToScrap();

    var request = new Request("/ship_design/scrap", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow",
        body: JSON.stringify(form),
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    await fetch(request).then(handleErrorCodesAndThenFollowRedirects);
}

function getShipsToScrap() {
    let form = new Object();
    form.items = [];

    let designs = $("input[name|='amount']");
    for (let i = 0; i < designs.length; i++) {
        let design = parseInt(designs[i].getAttribute("id"));
        let amount = parseInt(designs[i].value);
        
        if (amount != 0) {
            let item = new Object();
            item.id = design;
            item.amount = amount;
            form.items.push(item);
        }
    }

    return form;
}
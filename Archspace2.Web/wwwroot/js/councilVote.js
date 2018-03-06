async function changeVoteAsync() {
    let element = document.querySelector("#vote");
    let votedId = parseInt(element[element.selectedIndex].value);

    var request = new Request("/player/" + votedId + "/vote", {
        method: "POST",
        mode: "same-origin",
        credentials: "include",
        redirect: "follow"
    });

    try {
        await fetch(request).then(handleErrorCodes).then(refreshLocation);
    }
    catch(error) {
        console.log(error);
    }
}
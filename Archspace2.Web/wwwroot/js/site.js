async function handleErrorCodes(fetchResponse) {
    if (!fetchResponse.ok) {
        throw Error(await fetchResponse.text());
    }

    return fetchResponse;
}

async function followRedirects(fetchResponse) {
    if (fetchResponse.redirected) {
        window.location = fetchResponse.url;
    }

    return fetchResponse;
}

async function refreshLocation(fetchResponse) {
    location.reload();

    return fetchResponse;
}
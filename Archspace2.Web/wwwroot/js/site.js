async function handleErrorCodes(fetchResponse) {
    if (!fetchResponse.ok) {
        throw Error(fetchResponse.statusText + "\n" + await fetchResponse.text());
    }
}

async function handleErrorCodesAndThenFollowRedirects(fetchResponse) {
    await handleErrorCodes(fetchResponse);
    if (fetchResponse.redirected) {
        window.location = fetchResponse.url;
    }
}
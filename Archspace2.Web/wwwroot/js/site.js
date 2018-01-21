async function handleErrorCodes(fetchResponse) {
    if (!fetchResponse.ok) {
        throw Error(fetchResponse.statusText + "\n" + await fetchResponse.text());
    }
}
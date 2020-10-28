function FileImportHandler() {

    const uri = 'api/FileImport';

    function getRackInfo(rackHash) {
        return $.getJSON(uri + "/getRackInfo", { rackHash: rackHash });
    }

    return {
        getRackInfo: getRackInfo,
    }
}
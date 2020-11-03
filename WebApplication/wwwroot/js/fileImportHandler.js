function FileImportHandler() {

    const uri = 'api/FileImport';

    function getRackInfo(rackHash) {
        return $.getJSON(uri + "/getRackInfo", { rackHash: rackHash });
    }

    function getDirectoryInfo(rackHash, subDirectory) {
        var dto = { rackHash: rackHash, subDirectory: subDirectory };
        return $.getJSON(uri + "/getDirectoryInfo", dto);
    }

    return {
        getRackInfo: getRackInfo,
        getDirectoryInfo: getDirectoryInfo,
    }
}
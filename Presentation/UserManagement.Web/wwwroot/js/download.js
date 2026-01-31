window.downloadFileFromBytes = (fileName, contentType, bytes) => {
    const blob = new Blob([new Uint8Array(bytes)], { type: contentType || "application/octet-stream" });
    const url = URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = fileName || "download";
    a.click();

    URL.revokeObjectURL(url);
};

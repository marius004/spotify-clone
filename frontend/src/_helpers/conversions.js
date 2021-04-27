export function audioToBase64(audioFile) {
    return new Promise((resolve, reject) => {
        let reader = new FileReader();
        reader.onerror = reject;
        reader.onload = (e) => resolve(e.target.result);
        reader.readAsDataURL(audioFile);
    });
}

export function imageToBase64(file, callback) {
    var reader = new FileReader();
    reader.onloadend = function() {
        callback(reader.result);
    }
    reader.readAsDataURL(file);
}
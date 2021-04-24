const timeFormatter = {
    formatSeconds,
};

/// format seconds to minute:seconds
function formatSeconds(sec) {
    const minutes = Math.floor(sec / 60).toFixed(0);
    const seconds = Math.floor(sec - 60 * minutes).toFixed(0);
    return `${minutes}:${seconds}`;
}

export default timeFormatter;
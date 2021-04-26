export function authHeader() {
    let user = JSON.parse(localStorage.getItem('user'));

    if (user && user.jwtToken)
        return { Authorization: 'Bearer ' + user.jwtToken };

    return {};
}
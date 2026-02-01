window.postSignIn = (url, user, pass, returnUrl) => {
    const form = document.createElement('form');
    form.method = 'POST';
    form.action = url + (returnUrl ? '?returnUrl=' + encodeURIComponent(returnUrl) : '');
    const u = document.createElement('input');
    u.type = 'hidden';
    u.name = 'UserName';
    u.value = user ?? '';
    form.appendChild(u);
    const p = document.createElement('input');
    p.type = 'hidden';
    p.name = 'Password';
    p.value = pass ?? '';
    form.appendChild(p);
    document.body.appendChild(form);
    form.submit();
};
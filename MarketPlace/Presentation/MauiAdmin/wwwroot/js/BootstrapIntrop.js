var modals = {};

// نمایش مودال + انتظار برای اتمام رویداد انیمیشن
function ShowModalWithEvent(id, eventName) {
    return new Promise((resolve) => {
        let option = { backdrop: 'static', focus: true, keyboard: false };

        if (!modals[id]) {
            modals[id] = new bootstrap.Modal(`#${id}`, option);
        }

        const modalElement = document.getElementById(id);
        const eventHandler = () => {
            modalElement.removeEventListener(eventName, eventHandler);
            resolve(); // اتمام رویداد و بازگشت Promise
        };

        modalElement.addEventListener(eventName, eventHandler, { once: true });
        modals[id].show();
    });
}

// پنهان کردن مودال + انتظار برای اتمام رویداد انیمیشن
function HideModalWithEvent(id, eventName) {
    return new Promise((resolve) => {
        if (!modals[id]) return resolve();

        const modalElement = document.getElementById(id);
        const eventHandler = () => {
            modalElement.removeEventListener(eventName, eventHandler);
            resolve(); // اتمام رویداد و بازگشت Promise
        };

        modalElement.addEventListener(eventName, eventHandler, { once: true });
        modals[id].hide();
    });
}
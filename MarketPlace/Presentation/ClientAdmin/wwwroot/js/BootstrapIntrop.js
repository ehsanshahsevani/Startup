var modals = {};

function ShowModalWithEvent(id, eventName) {
    return new Promise((resolve) => {
        const modalElement = document.getElementById(id);
        if (!modalElement) {
            console.error(`Modal element with ID '${id}' not found.`);
            return resolve();
        }

        const options = { backdrop: 'static', focus: true, keyboard: false };
        if (!modals[id]) {
            modals[id] = new bootstrap.Modal(`#${id}`, options);
        }

        const eventHandler = () => {
            clearTimeout(fallbackTimeout);
            modalElement.removeEventListener(eventName, eventHandler);
            resolve();
        };

        // Fallback in case event doesn't fire (max 2 seconds)
        const fallbackTimeout = setTimeout(() => {
            console.warn(`[Modal ${id}] '${eventName}' did not fire. Proceeding with fallback.`);
            modalElement.removeEventListener(eventName, eventHandler);
            resolve();
        }, 2000);

        // Ensure event handler is set BEFORE calling show()
        setTimeout(() => {
            modalElement.addEventListener(eventName, eventHandler, { once: true });
            modals[id].show();
        }, 10);
    });
}

function HideModalWithEvent(id, eventName) {
    return new Promise((resolve) => {
        const modalElement = document.getElementById(id);
        if (!modals[id] || !modalElement) {
            return resolve();
        }

        const eventHandler = () => {
            clearTimeout(fallbackTimeout);
            modalElement.removeEventListener(eventName, eventHandler);
            resolve();
        };

        const fallbackTimeout = setTimeout(() => {
            console.warn(`[Modal ${id}] '${eventName}' did not fire. Proceeding with fallback.`);
            modalElement.removeEventListener(eventName, eventHandler);
            resolve();
        }, 2000);

        setTimeout(() => {
            modalElement.addEventListener(eventName, eventHandler, { once: true });
            modals[id].hide();
        }, 10);
    });
}

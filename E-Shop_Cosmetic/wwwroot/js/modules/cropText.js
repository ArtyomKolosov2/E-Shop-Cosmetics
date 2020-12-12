export function cropTextArray(textArray, size, endCharacter) {
    textArray.forEach(el => {
        let text = el.innerHTML;

        if (el.innerHTML.length > size) {
            text = text.substr(0, size);
            el.innerHTML = text + endCharacter;
        }
    });

};

export function cropText(text, size, endCharacter) {
    let data = text.innerHTML;

    if (data.length > size) {
        data = data.substr(0, size);
        text.innerHTML = data + endCharacter;
    }

};
const countNumber = (priceMaxRange) => {
    let sum = 0;
    for (let i = 1; i < priceMaxRange; i *= 10) {
        sum++;
    }
    return sum;
}

export function buildSlider(priceMinRange, priceMaxRange, slider) {
    let priceMin = document.getElementById('priceMin');
    let priceMax = document.getElementById('priceMax');

    priceMin.oninput = function () {
        if (priceMin.value < countNumber(priceMinRange)) {
            priceMin.value = priceMinRange;
        }
        // up range
        else if (priceMin.value > priceMaxRange) {
            if (priceMin.value.length === countNumber(priceMaxRange)) {
                priceMin.value = priceMaxRange - 1;
            }
            else {
                priceMin.value = priceMax.value.slice(0, countNumber(priceMaxRange)) - 1;
            }
        }
        slider.from_pretty = priceMin.value; render();
    }
    priceMax.oninput = function () {
        if (priceMax.value > priceMaxRange) {
            if (priceMax.value.length === countNumber(priceMaxRange)) {
                priceMax.value = priceMaxRange;
            }
            else {
                priceMax.value = priceMax.value.slice(0, countNumber(priceMaxRange));
            }
        }
        slider.to_pretty = priceMax.value; render();
    }


    const render = () => slider.ionRangeSlider({
        type: "double",
        grid: true,
        min: priceMinRange,
        max: priceMaxRange,
        from: priceMinRange,
        to: priceMinRange + 10,
        step: 1,
        postfix: "br",
        onStart: function (data) {
            data.min = priceMin;
            data.max = priceMax;
        },
        onChange: function (data) {
            priceMin.value = data.from_pretty;
            priceMax.value = data.to_pretty;
        },

    });
    render();
}
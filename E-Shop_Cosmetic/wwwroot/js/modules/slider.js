import { getCounterProducts, getCostProducts } from "./getDataFromCookie";

const countNumber = (Range) => {
    let sum = 0;
    for (let i = 1; i < Range; i *= 10) {
        sum++;
    }
    return sum;
}

export function buildSlider(minRange, maxRange, $slider) {
    let $inputFrom = $("#priceMin");
    let $inputTo = $("#priceMax");
    let instance;
    let from = 0;
    let to = 0;

    $slider.ionRangeSlider({
        skin: "flat",
        type: "double",
        grid: true,
        postfix: "br",
        min: minRange,
        max: maxRange,
        from: minRange,
        to: minRange + 10,
        onStart: updateInputs,
        onChange: updateInputs,
        onFinish: updateInputs
    });

    instance = $slider.data("ionRangeSlider");

    function updateInputs(data) {
        from = data.from;
        to = data.to;

        $inputFrom.prop("value", from);
        $inputTo.prop("value", to);
    }

    $inputFrom.on("input", function () {
        let value = $(this).prop("value");

        /* validate */

        // down range
        if (value < countNumber(minRange)) {
            value = minRange;
        }
        // up range
        else if (value > maxRange) {
            if (value.length === countNumber(maxRange)) {
                value = maxRange - 1;
            }
            else {
                value = String(maxRange).slice(0, countNumber(maxRange)) - 1;
            }
        }
        // if zero
        else if (value[0] == 0, value.length > 1) {
            value = value.slice(0, 3);
        }

        instance.update({
            from: value
        });

        $(this).prop("value", value);
    });

    $inputTo.on("input", function () {
        var value = $(this).prop("value");

        // validate
        if (value > maxRange) {
            if (value.length === countNumber(maxRange)) {
                value = maxRange;
            }
            else {
                value = value.slice(0, countNumber(maxRange));
            }
        }

        instance.update({
            to: value
        });

        $(this).prop("value", value);
    });
}
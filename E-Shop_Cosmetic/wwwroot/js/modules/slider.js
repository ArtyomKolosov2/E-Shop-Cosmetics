export function buildSlider(minRange, maxRange, $slider) {
    let $inputFrom = $("#priceMin");
    let $inputTo = $("#priceMax");
    let instance;
    let from = 0;
    let to = 0;

    $slider.ionRangeSlider({
        skin: "flat",
        type: "double",
        step: 0.1,
        grid: true,
        postfix: "br",
        min: minRange,
        max: maxRange,
        from: localStorage.getItem("SliderFrom") | minRange,
        to: localStorage.getItem("SliderTo") | minRange + 10,
        onStart: updateScroll,
        onChange: updateScroll,
        onFinish: updateScroll
    });

    instance = $slider.data("ionRangeSlider");

    function updateScroll(data) {
        from = data.from;
        to = data.to;
        $inputFrom.prop("value", from);
        $inputTo.prop("value", to);

        localStorage.setItem("SliderFrom", from);
        localStorage.setItem("SliderTo", to);
    }

    $inputFrom.on("input", function () {
        let value = $(this).prop("value");

        /* validate */

        // down range
        if (value < minRange.length) {
            value = minRange;
        }
        // up range
        else if (value > maxRange) {
            if (value.length === maxRange.length) {
                value = to - 1;
            }
            else {
                value = String(to).slice(0, to.length) - 1;
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
        localStorage.setItem("SliderFrom", value);
    });

    $inputTo.on("input", function () {
        let value = $(this).prop("value");

        // validate
        if (value > maxRange) {
            if (value.length === maxRange.length) {
                value = maxRange;
            }
            else {
                value = value.slice(0, maxRange.length);
            }
        }

        instance.update({
            to: value
        });

        $(this).prop("value", value);
        localStorage.setItem("SliderTo", value);
    });
}
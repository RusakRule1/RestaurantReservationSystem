(function () {
    const dateInput = document.querySelector('[name="Input.Date"]');
    const partySizeInput = document.getElementById('partySizeInput');
    const privateDiningCheck = document.getElementById('privateDiningCheck');
    const privateDiningSection = document.getElementById('privateDiningSection');
    const timeSlotSelect = document.getElementById('timeSlotSelect');

    function isFridayOrSaturday(dateStr) {
        const day = new Date(dateStr).getDay();
        return day === 5 || day === 6;
    }

    function updatePartySizeLimits() {
        const isPrivate = privateDiningCheck && privateDiningCheck.checked;
        if (isPrivate) {
            partySizeInput.min = 6;
            partySizeInput.max = 12;
            if (partySizeInput.value < 6) partySizeInput.value = 6;
            if (partySizeInput.value > 12) partySizeInput.value = 12;
        } else {
            partySizeInput.min = 1;
            partySizeInput.max = 10;
            if (partySizeInput.value > 10) partySizeInput.value = 10;
        }
    }

    async function loadSlots() {
        const date = dateInput.value;
        const partySize = partySizeInput.value;
        const isPrivate = privateDiningCheck ? privateDiningCheck.checked : false;

        if (!date || !partySize) {
            timeSlotSelect.innerHTML = '<option value="">— select date and party size first —</option>';
            timeSlotSelect.disabled = true;
            return;
        }

        timeSlotSelect.innerHTML = '<option value="">Loading...</option>';
        timeSlotSelect.disabled = true;

        try {
            const res = await fetch(
                `/?handler=AvailableSlots&date=${date}&partySize=${partySize}&isPrivateDining=${isPrivate}`
            );
            const slots = await res.json();

            if (slots.length === 0) {
                timeSlotSelect.innerHTML = '<option value="">No available slots</option>';
            } else {
                timeSlotSelect.innerHTML = '<option value="">— select a time —</option>' +
                    slots.map(s => `<option value="${s}">${s}</option>`).join('');
                timeSlotSelect.disabled = false;
            }
        } catch {
            timeSlotSelect.innerHTML = '<option value="">Error loading slots</option>';
        }
    }

    function onDateChange() {
        const date = dateInput.value;
        if (date && isFridayOrSaturday(date)) {
            privateDiningSection.style.display = 'block';
        } else {
            privateDiningSection.style.display = 'none';
            if (privateDiningCheck) privateDiningCheck.checked = false;
            updatePartySizeLimits();
        }
        loadSlots();
    }

    if (dateInput) dateInput.addEventListener('change', onDateChange);
    if (partySizeInput) partySizeInput.addEventListener('change', loadSlots);
    if (privateDiningCheck) {
        privateDiningCheck.addEventListener('change', function () {
            updatePartySizeLimits();
            loadSlots();
        });
    }

    const specialRequests = document.querySelector('[name="Input.SpecialRequests"]');
    const charCount = document.getElementById('charCount');
    if (specialRequests && charCount) {
        specialRequests.addEventListener('input', function () {
            charCount.textContent = `${this.value.length} / 500`;
        });
    }
})();

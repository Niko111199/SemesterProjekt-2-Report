       function updateCharCount() {
            const content = document.getElementById("content").innerText;
            document.getElementById("charCounter").innerText = "Antal Tegn: " + content.length;
        }
        
        document.addEventListener("input", updateCharCount);
        updateCharCount();
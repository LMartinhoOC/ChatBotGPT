public class CookieStorageAccessor {
    private Lazy<IJSObjectReference> _accessorJsRef = new ();
    private readonly IJSRuntime _jsRuntime;

    public CookieStorageAccessor(IJSRuntime jsRuntime) {
        _jsRuntime = jsRuntime;
    }

    private async Task WaitForReference() {
        if (_accessorJsRef.IsValueCreated is false)
        {
            _accessorJsRef = new (await _jsRuntime.InvokeAsync < IJSObjectReference > ("import", "/js/CookieStorageAccessor.js"));
        }
    }

    public async ValueTask DisposeAsync() {
        if (_accessorJsRef.IsValueCreated) {
            await _accessorJsRef.Value.DisposeAsync();
        }
    }
}
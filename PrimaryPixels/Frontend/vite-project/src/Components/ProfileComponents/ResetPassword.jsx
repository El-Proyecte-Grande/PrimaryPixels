import "./ResetPassword.scss";

export default function ResetPassword({ setResetPwd, setResetPasswordData, resetPassword, errorMessage }) {
    return (
        <>
            <form className="reset-pwd-form" onSubmit={(e) => resetPassword(e)}>
                <div className="pwd-form-headers">
                    <button type="button" className="exit-button" onClick={() => setResetPwd(prev => !prev)}> X </button>
                    {errorMessage != "" && <p className="error-message"> {errorMessage} </p>}
                </div>
                <div className="container">
                    <label className="change-label" htmlFor="current-password-input"> Current password</label>
                    <input className="change-input" type="password" id="current-password-input" onChange={(e) => setResetPasswordData(prev => ({ ...prev, currentPassword: e.target.value }))} />
                    <label className="change-label" htmlFor="new-password-input"> New password</label>
                    <input className="change-input" type="password" id="new-password-input" onChange={(e) => setResetPasswordData(prev => ({ ...prev, newPassword: e.target.value }))} />
                    <label className="change-label" htmlFor="confirm-password-input"> Confirm new password</label>
                    <input className="change-input" type="password" id="confirm-password-input" onChange={(e) => setResetPasswordData(prev => ({ ...prev, confirmNewPassword: e.target.value }))} />
                    <button className="submit-button"> Change </button>
                </div>
            </form>
        </>
    )
}
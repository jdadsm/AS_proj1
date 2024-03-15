import LogoutLink from "../Components/LogoutLink.tsx";
import AuthorizeView, { AuthorizedUser } from "../Components/AuthorizeView.tsx";
import HelpDeskRecords from "../Components/HelpDeskRecords.tsx";

function HelpDeskHome() {
    return (
        <AuthorizeView>
            <span><LogoutLink>Logout <AuthorizedUser value="email" /></LogoutLink></span>
            <HelpDeskRecords />
        </AuthorizeView>
    );
}

export default HelpDeskHome;
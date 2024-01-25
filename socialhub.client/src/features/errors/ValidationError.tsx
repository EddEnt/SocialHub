import { Message } from "semantic-ui-react";

interface Props {
    errors: string[];
}
export default function ValidationError({ errors }: Props) {

    // If there are errors, display them in a Semantic UI Message component    

    return (
        <Message error>
            {errors && (
                <Message.List>
                    {errors.map((err: string, i) => (
                        <Message.Item key={i}>{err}</Message.Item>
                    ))}
                </Message.List>
            )}
        </Message>
    )
}
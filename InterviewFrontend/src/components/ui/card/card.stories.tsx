import type { Meta, StoryObj } from "@storybook/react";
import {
  Card,
  CardHeader,
  CardFooter,
  CardTitle,
  CardDescription,
  CardContent,
} from "./card";
import { Button } from "../button/button";

const meta = {
  title: "UI/Card",
  component: Card,
  parameters: {
    layout: "centered",
  },
  tags: ["autodocs"],
} satisfies Meta<typeof Card>;

export default meta;
type Story = StoryObj<typeof meta>;

export const Default: Story = {
  render: () => (
    <Card className="w-87.5">
      <CardHeader>
        <CardTitle>Card Title</CardTitle>
        <CardDescription>Card Description</CardDescription>
      </CardHeader>
      <CardContent>
        <p>Card Content</p>
      </CardContent>
    </Card>
  ),
};

export const WithFooter: Story = {
  render: () => (
    <Card className="w-87.5">
      <CardHeader>
        <CardTitle>Card Title</CardTitle>
        <CardDescription>Card Description</CardDescription>
      </CardHeader>
      <CardContent>
        <p>This is a card with a footer containing action buttons.</p>
      </CardContent>
      <CardFooter className="flex justify-between">
        <Button variant="outline">Cancel</Button>
        <Button>Confirm</Button>
      </CardFooter>
    </Card>
  ),
};

export const ProductCard: Story = {
  render: () => (
    <Card className="w-87.5">
      <CardHeader>
        <CardTitle>Premium Plan</CardTitle>
        <CardDescription>Best for professional teams</CardDescription>
      </CardHeader>
      <CardContent>
        <div className="text-3xl font-bold mb-4">$29/month</div>
        <ul className="space-y-2">
          <li className="flex items-center gap-2">
            <span className="text-green-500">✓</span> Unlimited projects
          </li>
          <li className="flex items-center gap-2">
            <span className="text-green-500">✓</span> Priority support
          </li>
          <li className="flex items-center gap-2">
            <span className="text-green-500">✓</span> Advanced analytics
          </li>
        </ul>
      </CardContent>
      <CardFooter>
        <Button className="w-full">Get Started</Button>
      </CardFooter>
    </Card>
  ),
};

export const NotificationCard: Story = {
  render: () => (
    <Card className="w-100">
      <CardHeader>
        <CardTitle>New Message</CardTitle>
        <CardDescription>You have a new message from John Doe</CardDescription>
      </CardHeader>
      <CardContent>
        <p className="text-sm text-muted-foreground">
          "Hey! I wanted to follow up on our conversation yesterday. Are you
          available for a quick call this afternoon?"
        </p>
        <p className="text-xs text-muted-foreground mt-4">2 hours ago</p>
      </CardContent>
      <CardFooter className="flex gap-2">
        <Button variant="outline" className="flex-1">
          Dismiss
        </Button>
        <Button className="flex-1">Reply</Button>
      </CardFooter>
    </Card>
  ),
};

export const SimpleCard: Story = {
  render: () => (
    <Card className="w-75 p-6">
      <div className="space-y-2">
        <h3 className="font-semibold">Simple Card</h3>
        <p className="text-sm text-muted-foreground">
          A simple card without predefined sections.
        </p>
      </div>
    </Card>
  ),
};
